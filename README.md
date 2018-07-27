# Xamarin.Android.Lite

Prototype/proof of concept of a "lite" Xamarin.Android that only
supports Xamarin.Forms.

_DISCLAIMER: I created this project during Microsoft's #HackWeek 2018.
It is not "a real thing" or endorsed/supported by Xamarin/Microsoft.
If you would like it to be "a real thing", show your support! Star
this Github repo, like the YouTube video, post on social media,
comment, etc.! Every bit helps!_

![Xamarin.Android.Lite](docs/Xamarin.Android.Lite.gif)

# How do I use it?

The easiest way to create a new project, is to use the Xamarin.Forms
project template in Visual Studio. Just check one platform, and delete
the platform-specific project.

Edit your project file to look something like:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <EmbeddedResource Include="**\*.png" />
    <PackageReference Include="Xamarin.Android.Lite" Version="0.1.0.36-preview" />
  </ItemGroup>
</Project>
```

Remove the existing `<PackageReference />` to Xamarin.Forms, as
Xamarin.Android.Lite is pinned to a specific version of Xamarin.Forms.

To run the app:
- Launch the emulator, or connect an Android device via USB
- Use the Android-specific MSBuild targets: `SignAndroidPackage`,
  `Install`, or `Run`.

Details on each target:
- `SignAndroidPackage` drops an APK file in `$(OutputPath)`
- `Install` deploys the APK to the connected device
- `Run` launches the main activity on the device

So, assuming the right `MSBuild.exe` is in your path on Windows (Mac
will "just work"):

    msbuild MyApp.csproj /t:Run

This command gets you going!

Visit the [MSBuild documentation](docs/MSBuild.md) for further details
about MSBuild (project) properties.

# What's in the box?

Xamarin.Android.Lite is bundled with Xamarin.Forms and
Xamarin.Essentials to get the best APIs available for NetStandard.

Currently using:

    <PackageReference Include="Xamarin.Forms" Version="3.1.0.583944" />
    <PackageReference Include="Xamarin.Essentials" Version="0.8.0-preview" />

If another library is deemed useful here, let me know--I could bundle
it!

# How do I use images?

As noted in the project file above, `<EmbeddedResource />` is the way
to go:

    <EmbeddedResource Include="**\*.png" />

Then to load the image, you will need to use the following C#:

    yourImage.Source = ImageSource.FromResource ("YourNameSpace.xamarin_logo.png", typeof (App));

Or *better yet*, make your own XAML markup extension to do this!

# How do I contribute? Or build this repo?

    msbuild build.proj /t:Bootstrap
    msbuild build.proj

Another useful target, which doesn't invalidate `bin\xamarin-android`:

    msbuild build.proj /t:Clean

The `Bootstrap` target will take a while. An OSS build of
xamarin-android will be downloaded and extracted into
`bin\xamarin-android`.

I did this originally because I thought I may need to leverage
Xamarin.Android artifacts from there.

Right now I am only using:
- Xamarin.Android.Tools.AndroidSdk.dll
- libzip.dll (binaries for each platform)
- libZipSharp.dll

So maybe downloading `xamarin-android` could be removed eventually?