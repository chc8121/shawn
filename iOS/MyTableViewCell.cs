using System;

using Foundation;
using UIKit;

namespace shawn.iOS
{
	public partial class MyTableViewCell : UITableViewCell
	{
		protected MyTableViewCell(IntPtr handle) : base(handle)
		{
			
		}

		public void updateUI(Todo todo)
		{
			lbName.Text = todo.Name;
			lbDescription.Text = todo.Description;
		}
	}
}
