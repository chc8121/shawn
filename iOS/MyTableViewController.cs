using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


using UIKit;

namespace shawn.iOS
{
	public partial class MyTableViewController : UIViewController
	{
		public MyTableViewController(IntPtr handle) : base (handle)
		{
			Title = "Table";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ShowTable();
		}

		private void ShowTable()
		{
			var list = new List<Todo>();

			list.Add(new Todo { Name = "了解IOC", Description = "控制反轉" });
			list.Add(new Todo { Name = "了解DI", Description = "依賴注入" });
			list.Add(new Todo { Name = "了解UI Test", Description = "準備" });
			list.Add(new Todo { Name = "了解Unit Test", Description = "達成" });

			var todoSource = new TodoSource(list);
			myTable.Source = todoSource;
			todoSource.TodoSelected += (object sender, TodoSelectedEventArgs e) =>
			{
				Debug.WriteLine($"TableSelected Name:{e.SelectedTodo.Name}, Description:{e.SelectedTodo.Description}");
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		class TodoSource : UITableViewSource 
		{
			private List<Todo> Todoes { get; set; }

			public event EventHandler<TodoSelectedEventArgs> TodoSelected;

			const string MyTableViewCellIdentifier = "MyTableViewCell";

			public TodoSource(IEnumerable<Todo> source)
			{
				Todoes = new List<Todo>();
				Todoes.AddRange(source);
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Todoes.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				MyTableViewCell cell = tableView.DequeueReusableCell(MyTableViewCellIdentifier, indexPath) as MyTableViewCell;

				var todo = Todoes[indexPath.Row];
				cell.updateUI(todo);

				return cell;
			}

			public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				tableView.DeselectRow(indexPath, true);

				var todo = Todoes[indexPath.Row];

				EventHandler<TodoSelectedEventArgs> handle = TodoSelected;

				if (null != handle)
				{
					handle(this, new TodoSelectedEventArgs { SelectedTodo = todo });
				}
			}
		}

		public class TodoSelectedEventArgs : EventArgs
		{
			public Todo SelectedTodo { get; set; }

		}
	}
}


