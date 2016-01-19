# MyoUniversalPlugin
Unofficial Myo Plugin which enables you to build Unity applications for Windows, Android and iOS.
## Directory structure and Setup
Just import the three folders in your Unity Project. This repository is a new version to the MyoUnityAndroidPlugin repository. I updated it to Unity 5.3.1 due to some requests i got.
I tested the plugin with Unity 5.3.1 and a Galaxy Note 4. I just finished some projects with GearVR Support + Myo, which worked nicely. Although I used Myo Android SDK 0.10.0 and Myo software version 1.0.0 to build the project. The "AndroidPlugin" itself is built as an "Android Bound Service" so it is mentioned in the Manifest file but not as the main-application. This should enable you to use a second plugin more easily.

```
\Assets
    +---Myo
    \---Myo Samples
    \---Plugins
```

## Changes to the old Plugin

You can now use "AttachByMacAddress", which allows your application to attach your myo device from start. You have to change the MAC Address in ThalmicHub.cs and call "ThalmicHub.AttachByMacAddress()" in one of your scripts.
I used the Box On a Stick demo this time, which is delivered with the official Thamlic Myo Unity Plugin. Its also a mix with a existing Plugin which supports iOS. I never tested it, cause i dont own any iOS devices, perhaps someone could do this for me.

## Getting Started

1. Import folders into your Unity project.
2. Open "MyoPlugin/Demo/Scenes/Box On a Stick.unity".
3. Change the Mac Address in ThalmicHub.cs
3. In your Build Settings switch your platform to Android.
4. In Player Settings you have to set your Bundle Identifier
5. Also set minimum API Level 18 (Android 4.3 'Jelly Bean'). Everything else in Player Settings should be optional.
6. Now you should be able to build and deploy to your Android device.

## The API

When you build your own scene, you simply need to have a GameObject thats called "MyoManager" with the MyoManger script attached to it. For an out of the box solution, there is also a prefab included which you can drag into your scene. You now can start the plugin via "MyoManager.Initialize", the other methods are called in the same manner. The code below represents the methods which can be called using the MyoManger.

## Known Issues

- The GUI on the demo scene is showing "No Connection" on the Android device. This is caused by some architecture changes I had to implement for the Android support. Perhaps I can fix this in the future. For Windows it should work nicely.
- Currently you can only use one Myo per device. If I can get my hands on a second one i will implement this functionality too.

## Support

This project was kind of rushed, so if you have any feedback just contact me (Florian.Strieg@Student.Reutlingen-University.DE). Although i cannot guarantee any direct support I'm looking forward to develop this plugin a bit further.

## Thanks

Last but not least I would like you to know that I took inspiration and parts of code from  https://github.com/zoiclabs/Myo-Unity-iOS-Plugin. I liked the architecture so hopefully the creator will be okay with it =)
