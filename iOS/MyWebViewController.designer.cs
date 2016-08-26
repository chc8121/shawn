// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace shawn.iOS
{
	[Register ("MyWebViewController")]
	partial class MyWebViewController
	{
		[Outlet]
		UIKit.NSLayoutConstraint btnGoButtonConstraint { get; set; }

		[Outlet]
		UIKit.UIButton button { get; set; }

		[Outlet]
		UIKit.UITextField textField { get; set; }

		[Outlet]
		UIKit.UIWebView webview { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGoButtonConstraint != null) {
				btnGoButtonConstraint.Dispose ();
				btnGoButtonConstraint = null;
			}

			if (button != null) {
				button.Dispose ();
				button = null;
			}

			if (textField != null) {
				textField.Dispose ();
				textField = null;
			}

			if (webview != null) {
				webview.Dispose ();
				webview = null;
			}
		}
	}
}
