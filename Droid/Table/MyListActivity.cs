
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace shawn.Droid
{
	[Activity(Label = "MyListActivity")]
	public class MyListActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.table_mylistview);

			ShowTable();
		}

		private void ShowTable()
		{
			var list = new List<Todo>();

			list.Add(new Todo { Name = "了解IOC", Description = "控制反轉" });
			list.Add(new Todo { Name = "了解DI", Description = "依賴注入" });
			list.Add(new Todo { Name = "了解UI Test", Description = "準備" });
			list.Add(new Todo { Name = "了解Unit Test", Description = "達成" });

			var listView = FindViewById<ListView>(Resource.Id.tableview_mylistview);

			listView.Adapter = new TodoAdapter(list, this);

			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				System.Diagnostics.Debug.WriteLine($"TableSelected Name:{list[e.Position].Name}, Description:{list[e.Position].Description}");
			};
		}

		class TodoAdapter : BaseAdapter<Todo>
		{
			private List<Todo> Todoes { get; set; }
			private Activity Context { get; set; }

			public TodoAdapter(IEnumerable<Todo> source, Activity context)
			{
				Todoes = new List<Todo>();
				Todoes.AddRange(source);

				Context = context;
			}

			public override int Count => Todoes.Count;

			public override Todo this[int position] => Todoes[position];

			public override long GetItemId(int position)
			{
				return position;
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				var view = convertView;

				if (null == view)
				{
					view = this.Context.LayoutInflater.Inflate(Resource.Layout.table_mylistview_itemview, parent, false);
				}

				var todo = Todoes[position];

				view.FindViewById<TextView>(Resource.Id.table_mylistview_itemview_name).Text = todo.Name;
				view.FindViewById<TextView>(Resource.Id.table_mylistview_itemview_description).Text = todo.Description;

				return view;
			}
		}
	}
}

