﻿using NUnit.Framework;
using System.IO;
using Xamarin.Android.Lite.Tasks;

namespace Xamarin.Android.Lite.Tests
{
	[TestFixture]
	public class GenerateManifestTests
	{
		MockBuildEngine engine;
		string temp;

		[SetUp]
		public void SetUp ()
		{
			temp = Path.GetTempFileName ();
			engine = new MockBuildEngine ();
		}

		[TearDown]
		public void TearDown ()
		{
			File.Delete (temp);
		}

		[Test]
		public void Generate ()
		{
			var task = new GenerateManifest {
				BuildEngine = engine,
				DestinationFile = temp,
				PackageName = "com.test.app",
				ApplicationClass = "My.Namespace.App, MyAssembly",
				ActivityName = "xamarin.android.lite.MainActivity",
				VersionCode = "12",
				VersionName = "2.0.0",
				AppTitle = "MyApp",
				ActivityTitle = "MyActivity",
			};
			Assert.IsTrue (task.Execute (), "Execute failed!");

			using (var stream = File.OpenRead (temp)) {
				var manifest = AndroidManifest.Read (stream);
				var xml = @"<manifest xmlns:android=""http://schemas.android.com/apk/res/android"" android:versionCode=""12"" android:versionName=""2.0.0"" package=""com.test.app"" platformBuildVersionCode=""27"" platformBuildVersionName=""8.1.0"">
  <uses-sdk android:minSdkVersion=""19"" android:targetSdkVersion=""27"" />
  <uses-permission android:name=""android.permission.INTERNET"" />
  <uses-permission android:name=""android.permission.READ_EXTERNAL_STORAGE"" />
  <uses-permission android:name=""android.permission.WRITE_EXTERNAL_STORAGE"" />
  <uses-permission android:name=""android.permission.ACCESS_NETWORK_STATE"" />
  <uses-permission android:name=""android.permission.BATTERY_STATS"" />
  <uses-permission android:name=""android.permission.CAMERA"" />
  <uses-permission android:name=""android.permission.FLASHLIGHT"" />
  <uses-permission android:name=""android.permission.ACCESS_COARSE_LOCATION"" />
  <uses-permission android:name=""android.permission.ACCESS_FINE_LOCATION"" />
  <uses-permission android:name=""android.permission.VIBRATE"" />
  <uses-feature android:name=""android.hardware.location"" android:required=""false"" />
  <uses-feature android:name=""android.hardware.location.gps"" android:required=""false"" />
  <uses-feature android:name=""android.hardware.location.network"" android:required=""false"" />
  <application android:label=""MyApp"" android:icon=""2130903040"" android:name=""android.app.Application"" android:debuggable=""true"" android:allowBackup=""true"">
    <meta-data android:name=""Xamarin.Android.Lite.Application"" android:value=""My.Namespace.App, MyAssembly"" />
    <activity android:theme=""2131493263"" android:label=""MyActivity"" android:icon=""2130903040"" android:name=""xamarin.android.lite.MainActivity"" android:configChanges=""1152"">
      <intent-filter>
        <action android:name=""android.intent.action.MAIN"" />
        <category android:name=""android.intent.category.LAUNCHER"" />
      </intent-filter>
    </activity>
    <service android:name=""md5dcb6eccdc824e0677ffae8ccdde42930.KeepAliveService"" />
    <receiver android:label=""Essentials Battery Broadcast Receiver"" android:name=""md5d630c3d3bfb5f5558520331566132d97.BatteryBroadcastReceiver"" android:enabled=""true"" android:exported=""false"" />
    <receiver android:label=""Essentials Connectivity Broadcast Receiver"" android:name=""md5d630c3d3bfb5f5558520331566132d97.ConnectivityBroadcastReceiver"" android:enabled=""true"" android:exported=""false"" />
    <receiver android:label=""Essentials Power Broadcast Receiver"" android:name=""md5d630c3d3bfb5f5558520331566132d97.PowerBroadcastReceiver"" android:enabled=""true"" android:exported=""false"" />
    <receiver android:name=""md51558244f76c53b6aeda52c8a337f2c37.PowerSaveModeBroadcastReceiver"" android:enabled=""true"" android:exported=""false"" />
    <provider android:name=""mono.MonoRuntimeProvider"" android:exported=""false"" android:authorities=""com.test.app.mono.MonoRuntimeProvider.__mono_init_"" android:initOrder=""2147483647"" />
    <receiver android:name=""mono.android.Seppuku"">
      <intent-filter>
        <action android:name=""mono.android.intent.action.SEPPUKU"" />
        <category android:name=""mono.android.intent.category.SEPPUKU.com.test.app"" />
      </intent-filter>
    </receiver>
  </application>
</manifest>";
				Assert.AreEqual (xml, manifest.Document.ToString ());
			}
		}
	}
}
