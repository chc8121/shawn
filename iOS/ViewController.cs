using System;

using UIKit;

namespace shawn.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
			Title = "Main";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				PerformSegue("moveToMapSegue", this);
			};

			btnConfirm.TouchUpInside += (sender, e) =>
			{
				PerformSegue("moveToWebSegue", this);
			};

			_btnTable.TouchUpInside += (sender, e) =>
			{
				PerformSegue("moveToTableSegue", this);
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
