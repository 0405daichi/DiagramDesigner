using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiagramDesignerApp
{
    class RelayCommand : ICommand
    {
        /// <summary>実行されるアクション</summary>
        private Action<object> action;

        /// <summary>実行可否を判断するファンクション</summary>
        private Func<bool> canExecute = null;

        /// <summary>
        /// 実行可能かどうかを確認する状態が変更されたときのイベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="action">実行されるアクション</param>
        public RelayCommand(Action<object> action)
            : this(action, null)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="action">実行されるアクション</param>
        /// <param name="canExecute">実行可能を確認するファンクション</param>
        public RelayCommand(Action<object> action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// コマンドが実行可能か
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute.Invoke();
            }
        }

        /// <summary>
        /// コマンド実行ルーチン
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (action != null)
            {
                action(parameter);
            }
        }

        /// <summary>
        /// CanExecuteChangedイベントを発生させます
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
