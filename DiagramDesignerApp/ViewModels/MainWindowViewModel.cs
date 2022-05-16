using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows;
using System.ComponentModel;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;

namespace DiagramDesignerApp
{
    public class MainWindowViewModel : INPCBase
    {
        private DiagramViewModel diagramViewModel;
        public MainWindowViewModel()
        {
            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new DiagramViewModel();
            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();
            SerializeCommand = new SimpleCommand(SerializeItems);
            DeserializeCommand = new SimpleCommand(DeserializeItems);
            DeleteCommand = new SimpleCommand(DeleteSelectedItems);
        }

        public ToolBoxViewModel ToolBoxViewModel { get; private set; }
        public SimpleCommand SerializeCommand { get; private set; }
        public SimpleCommand DeserializeCommand { get; private set; }
        public SimpleCommand DeleteCommand { get; private set; }

        public DiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel == value)
                {
                    return;
                }

                diagramViewModel = value;
                NotifyChanged("DiagramViewModel");
            }
        }

        // Jsonファイルを書き込み保存する処理
        public void SerializeItems(object param)
        {
            if (DiagramViewModel.Items.Any())
            {
                var serializeItems = new List<SerializeItems>();
                foreach (var item in DiagramViewModel.Items)
                {
                    if (item is HumanDesignerItemViewModel || item is PinDesignerItemViewModel)
                    {
                        continue;
                    }
                    var pathData = new SerializeItems()
                    {
                        Type = item.Type,
                        Name = item.ParentPathName,
                        StartPoint = item.Start.Point,
                        EndPoint = item.End.Point,
                        StartPointAccessLines = CreateAccessLineData(item, "Start"),
                        EndPointAccessLines = CreateAccessLineData(item, "End")
                    };
                    if (item is CircleDesignerItemViewModel)
                    {
                        pathData.CenterPoint = item.Center.Point;
                    }
                    serializeItems.Add(pathData);
                }
                if (serializeItems.Any())
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
                    };
                    var jsonString = JsonSerializer.Serialize(serializeItems, options);
                    MessageBox.Show(jsonString);
                    var result = MessageBox.Show("保存しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        saveFileDialog.Title = "ファイルを保存する";
                        saveFileDialog.InitialDirectory = @"C:\Users";
                        saveFileDialog.FileName = @"test.json";
                        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string path = saveFileDialog.FileName;
                            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.WriteLine(jsonString);
                                }
                            }
                        }
                    }
                }
            }
        }

        // 与えたクラス名からTypeを取得
        public static Type GetTypeByClassName(string className)
        {
            foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)
                    {
                        return type;
                    }
                }
            }
            return null;
        }

        // Jsonファイルを読み込みアイテムを配置する処理
        public void DeserializeItems(object param)
        {
            var deserializeItems = new List<SerializeItems>();
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var stream = openFileDialog.OpenFile())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            string s = null;
                            while (!reader.EndOfStream)
                            {
                                s += reader.ReadLine() + "\n";
                            }
                            MessageBox.Show(s);
                            deserializeItems = JsonSerializer.Deserialize<List<SerializeItems>>(s);
                        }
                    }
                }

                var flag = true;

                // デシアライズする際に画面上にアイテムが存在する場合の処理
                if (DiagramViewModel.Items.Any())
                {
                    var message = "現在配置されてあるアイテムは削除されます。";
                    var result = MessageBox.Show(message, "確認", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DiagramViewModel.Items.Clear();
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag)
                {

                    var addItems = new List<SelectableDesignerItemViewModelBase>();

                    // 読み込んだデータを適する型へ変換
                    foreach (var item in deserializeItems)
                    {
                        var type = GetTypeByClassName(item.Type);
                        var method = type.GetMethod("Deserialize");
                        if (method != null)
                        {
                            var obj = method.Invoke(null, new object[] { item }) as SelectableDesignerItemViewModelBase;
                            addItems.Add(obj);
                        }
                    }

                    // 読み込んだデータから接続情報をアイテムに保持させる
                    foreach (var item in deserializeItems)
                    {
                        if (item.StartPointAccessLines.Any())
                        {
                            var connectedItems = new List<SelectableDesignerItemViewModelBase>();
                            foreach (var i in item.StartPointAccessLines)
                            {
                                connectedItems.Add(addItems.Find(v => i.Contains(v.ParentPathName)));
                            }
                            addItems.Find(v => v.ParentPathName == item.Name).Start.AccessLine = connectedItems;
                        }
                        if (item.EndPointAccessLines.Any())
                        {
                            var connectedItems = new List<SelectableDesignerItemViewModelBase>();
                            foreach (var i in item.EndPointAccessLines)
                            {
                                connectedItems.Add(addItems.Find(v => i.Contains(v.ParentPathName)));
                            }
                            addItems.Find(v => v.ParentPathName == item.Name).End.AccessLine = connectedItems;
                        }
                    }

                    // データを読み込み型変換、接続情報保持を済ませたアイテムを追加(画面に表示)
                    foreach (var addItem in addItems)
                    {
                        DiagramViewModel.AddItemCommand.Execute(addItem);
                    }
                }
            }
        }

        private void DeleteSelectedItems(object param)
        {
            if (DiagramViewModel.SelectedItems.Any())
            {
                foreach (var item in DiagramViewModel.SelectedItems)
                {
                    DiagramViewModel.RemoveItemCommand.Execute(item);
                }
            }
        }

        // 端点の接続情報をJsonファイルに記述できるよう補正する処理
        private List<string> CreateAccessLineData(SelectableDesignerItemViewModelBase item, string name)
        {
            var startAccessLines = new List<string>();
            var endAccessLines = new List<string>();
            if (item.Start.AccessLine.Any())
            {
                foreach (var i in item.Start.AccessLine)
                {
                    string pointName = "";
                    foreach (var j in i.Start.AccessLine)
                    {
                        if (item.ParentPathName == j.ParentPathName)
                        {
                            pointName = "Start";
                        }
                    }
                    foreach (var j in i.End.AccessLine)
                    {
                        if (item.ParentPathName == j.ParentPathName)
                        {
                            pointName = "End";
                        }
                    }
                    startAccessLines.Add($"{i.ParentPathName}:{pointName}");
                }
            }
            if (item.End.AccessLine.Any())
            {
                foreach (var i in item.End.AccessLine)
                {
                    string pointName = "";
                    foreach (var j in i.Start.AccessLine)
                    {
                        if (item.ParentPathName == j.ParentPathName)
                        {
                            pointName = "Start";
                        }
                    }
                    foreach (var j in i.End.AccessLine)
                    {
                        if (item.ParentPathName == j.ParentPathName)
                        {
                            pointName = "End";
                        }
                    }
                    endAccessLines.Add($"{i.ParentPathName}:{pointName}");
                }
            }

            if (name is "Start")
            {
                return startAccessLines;
            }
            else if (name is "End")
            {
                return endAccessLines;
            }
            else
            {
                return null;
            }
        }
    }
}
