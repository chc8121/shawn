using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Android.OS;
using Android.App;
using Android.Widget;
using Android.Webkit;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.Views.InputMethods;

using Java.Interop;

using AndroidHUD;
using Debug = System.Diagnostics.Debug;

namespace shawn.Droid
{
	[Activity(Label = "MyWebActivity")]
	public class MyWebActivity : Activity
	{
		private WebView myWebView { get; set; }
		private Button btnGo { get; set; }
		private EditText txtUrl { get; set; }

		private InputMethodManager _InputMethodManager;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			String url = Intent.GetStringExtra("Url");

			SetContentView(Resource.Layout.Web);

			myWebView = FindViewById<WebView> (Resource.Id.webView_MyWeb);
			btnGo = FindViewById<Button>(Resource.Id.btn_MyWeb_Go);
			txtUrl = FindViewById<EditText>(Resource.Id.editText_MyWeb_txtUrl);

			var client = new ContentWebViewClient();

			client.WebViewLocaitonChanged += (object sender, ContentWebViewClient.WebViewLocaitonChangedEventArgs e) =>
			{
			};

			client.WebViewLoadCompleted += (object sender, ContentWebViewClient.WebViewLoadCompletedEventArgs e) =>
			{
				RunOnUiThread(() =>
				{
					AndHUD.Shared.Dismiss(this);
				});
			};

			myWebView.SetWebViewClient(client);
			//MyWebView.SetWebViewClient(new MyWebClient());
			myWebView.Settings.JavaScriptEnabled = true;
			myWebView.Settings.UserAgentString = @"Android";

			// 負責與頁面溝通 - WebView -> Native
			MyJSInterface myJSInterface = new MyJSInterface(this);

			myWebView.AddJavascriptInterface(myJSInterface, "TP");
			myJSInterface.CallFromPageReceived += delegate (object sender, MyJSInterface.CallFromPageReceivedEventArgs e)
			{
				Debug.WriteLine(e.Result);
			};

			// 負責與頁面溝通 - Native -> WebView
			JavaScriptResult callResult = new JavaScriptResult();
			callResult.JavaScriptResultReceived += (object sender, JavaScriptResult.JavaScriptResultReceivedEventArgs e) =>
			{

				Debug.WriteLine(e.Result);
			};

			myWebView.LoadUrl(url);

			_InputMethodManager =
				(InputMethodManager)GetSystemService(Context.InputMethodService);


			/*
			TxtUrl = FindViewById<EditText> (Resource.Id.txtUrl);

			TxtUrl.TextChanged += (object sender,
				Android.Text.TextChangedEventArgs e) => {

				Debug.WriteLine( TxtUrl.Text +":"+ e.Text );

			};

			*/

			btnGo.Click += (object sender, EventArgs e) =>
			{

				RunOnUiThread(() =>
				{

					myWebView.EvaluateJavascript(@"msg();", callResult);
				});

				/*
				var url = TxtUrl.Text.Trim() ;

				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle (url);
				alert.SetNegativeButton( "取消", (senderAlert, args) =>{


				});
				alert.SetPositiveButton( "確認", (senderAlert, args) =>{

					RunOnUiThread(
						()=>{
							AndHUD.Shared.Show(this, "Status Message", -1, MaskType.Clear);
						}

					);

					MyWebView.LoadUrl (url);

				});

				RunOnUiThread (() => {
					alert.Show();
				} );

				// 
				_InputMethodManager.HideSoftInputFromWindow( 
					TxtUrl.WindowToken, 
					HideSoftInputFlags.None );
				*/
			};
		}

		public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
		{
			return base.OnKeyDown(keyCode, e);
		}

		// Call from Page
		public class MyJSInterface : Java.Lang.Object
		{
			Context Context { get; set; }

			public MyJSInterface(Context context)
			{
				this.Context = context;
			}

			[Export]
			[JavascriptInterface]
			public void CallFromPage(string parameter)
			{
				Debug.WriteLine($"CallFromPage:{parameter}");

				EventHandler<CallFromPageReceivedEventArgs> handler =
					CallFromPageReceived;

				if (null != handler)
				{
					handler(this,
						new CallFromPageReceivedEventArgs
						{
							Result = parameter
						});
				}
			}

			public event EventHandler<CallFromPageReceivedEventArgs> CallFromPageReceived;

			public class CallFromPageReceivedEventArgs : EventArgs
			{
				public string Result { get; set; }
			}
		}


		// Call Page
		public class JavaScriptResult : Java.Lang.Object, IValueCallback
		{

			public void OnReceiveValue(Java.Lang.Object result)
			{
				Java.Lang.String json = (Java.Lang.String)result;

				var resultString = json.ToString();

				EventHandler<JavaScriptResultReceivedEventArgs> handler =
					JavaScriptResultReceived;

				if (null != handler)
				{
					handler(this,
						new JavaScriptResultReceivedEventArgs
						{
							Result = resultString ?? ""
						});
				}
			}

			public event EventHandler<JavaScriptResultReceivedEventArgs> JavaScriptResultReceived;

			public class JavaScriptResultReceivedEventArgs : EventArgs
			{
				public string Result { get; set; }
			}

		}

		public class ContentWebViewClient : WebViewClient
		{

			public event EventHandler<WebViewLocaitonChangedEventArgs> WebViewLocaitonChanged;

			public event EventHandler<WebViewLoadCompletedEventArgs> WebViewLoadCompleted;

			public override bool ShouldOverrideUrlLoading(WebView view, string url)
			{
				EventHandler<WebViewLocaitonChangedEventArgs> handler =
					WebViewLocaitonChanged;

				if (null != handler)
				{
					handler(this,
						new WebViewLocaitonChangedEventArgs
						{
							CommandString = url
						});
				}

				return base.ShouldOverrideUrlLoading(view, url);

			}


			public override void OnPageFinished(WebView view, string url)
			{
				base.OnPageFinished(view, url);

				EventHandler<WebViewLoadCompletedEventArgs> handler =
					WebViewLoadCompleted;

				if (null != handler)
				{
					handler(this,
						new WebViewLoadCompletedEventArgs());
				}
			}

			public class WebViewLocaitonChangedEventArgs : EventArgs
			{

				public string CommandString { get; set; }
			}

			public class WebViewLoadCompletedEventArgs : EventArgs
			{

			}
		}
	}
}

