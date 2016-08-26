using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace shawn.Droid
{
	[Activity(Label = "shawn", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			var buttonMap = FindViewById<Button>(Resource.Id.btn_MainMenu_Map);

			var buttonWeb = FindViewById<Button>(Resource.Id.btn_MainMenu_Web);

			//buttonMap.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
			buttonWeb.Click += delegate 
			{
				Intent webActivity = new Intent(this, typeof(MyWebActivity));

				webActivity.PutExtra("Url", "https://stackoverflow.com");

				StartActivity(webActivity);	
			};

			var buttonList = FindViewById<Button>(Resource.Id.btn_MainMenu_List);
			buttonList.Click += delegate 
			{
				Intent listActivity = new Intent(this, typeof(MyListActivity));

				StartActivity(listActivity);
			};
		}
	}
}


