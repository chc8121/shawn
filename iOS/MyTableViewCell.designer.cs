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
	[Register ("MyTableViewCell")]
	partial class MyTableViewCell
	{
		[Outlet]
		UIKit.UILabel lbDescription { get; set; }

		[Outlet]
		UIKit.UILabel lbName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbName != null) {
				lbName.Dispose ();
				lbName = null;
			}

			if (lbDescription != null) {
				lbDescription.Dispose ();
				lbDescription = null;
			}
		}
	}
}
