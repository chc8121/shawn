using System;

using UIKit;
using Foundation;

namespace shawn.iOS
{
	public partial class MyWebViewController : UIViewController
	{
		public MyWebViewController(IntPtr handle) : base(handle)
		{
			Title = "Web";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			webview.LoadRequest(new NSUrlRequest(new NSUrl(@"https://www.google.com")));

			button.TouchUpInside += (sender, e) =>
			{
				if (textField.IsFirstResponder)
				{
					textField.ResignFirstResponder();
				}

				btnGoButtonConstraint.Constant = 20;
				if (textField.Text != null && textField.Text.Length > 0)
				{
					webview.LoadRequest(new NSUrlRequest(new NSUrl(textField.Text)));
				}
			};

			UIKeyboard.Notifications.ObserveWillChangeFrame((sender, args) =>
			{
				btnGoButtonConstraint.Constant = args.FrameEnd.Height;
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


