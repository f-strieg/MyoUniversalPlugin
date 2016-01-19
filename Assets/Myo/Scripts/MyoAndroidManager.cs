using UnityEngine;
using System.Collections;

using Thalmic;
using Pose = Thalmic.Myo.Pose;

public class MyoAndroidManager : MonoBehaviour {

#if UNITY_ANDROID

    public static bool isInitialized = false;
    public static bool isAttached = false;

    void Start()
    {
        Debug.Log("Starting Android Manager");
    }

    #region DelegateCalls
    //this are the methods which are called from java
    public void OnAttach(string message)
    {
        /*if (AttachEvent != null)
            AttachEvent();*/
        isAttached = true;
    }

    public void OnDetach(string message)
    {
        //if (DetachEvent != null)
        //    DetachEvent();
        isAttached = false;
    }

    public void OnConnect(string message)
    {
        //if (ConnectEvent != null)
        //    ConnectEvent();
        isAttached = true;
    }

    public void OnDisconnect(string message)
    {
        //if (DisconnectEvent != null)
        //    DisconnectEvent();
        isAttached = false;
    }

    public void OnArmSync(string message)
    {
        ThalmicMyo targetMyo = GetMyo("test");
        if (targetMyo != null)
        {
            if (ThalmicHub.DidSyncArm != null)
                ThalmicHub.DidSyncArm(targetMyo);

            targetMyo._myoXDirection = Thalmic.Myo.XDirection.TowardWrist;
            targetMyo._myoArmSynced = true;
        }
    }

    public void OnArmUnsync(string message)
    {
        ThalmicMyo targetMyo = GetMyo("test");
        if (targetMyo != null)
        {
            if (ThalmicHub.DidUnsyncArm != null)
                ThalmicHub.DidUnsyncArm(targetMyo);

            targetMyo._myoArmSynced = false;
        }
    }

    public void OnUnlock(string message)
    {
        ThalmicMyo targetMyo = GetMyo("test");
        if (targetMyo != null)
        {
            if (ThalmicHub.DidUnlockDevice != null)
                ThalmicHub.DidUnlockDevice(targetMyo);
            targetMyo._myoUnlocked = true;
        }
    }

    public void OnLock(string message)
    {
        ThalmicMyo targetMyo = GetMyo("test");
        if (targetMyo != null)
        {
            if (ThalmicHub.DidUnlockDevice != null)
                ThalmicHub.DidUnlockDevice(targetMyo);
            targetMyo._myoUnlocked = false;
        }
    }

    public void OnPose(string message)
    {
        Pose receivedPose = Pose.Unknown;
        Debug.Log("Unknown Pose: " + receivedPose);

        switch (message)
        {
            case "REST":
                receivedPose = Pose.Rest;
                break;
            case "FIST":
                receivedPose = Pose.Fist;
                break;
            case "WAVE_IN":
                receivedPose = Pose.WaveIn;
                break;
            case "WAVE_OUT":
                receivedPose = Pose.WaveOut;
                break;
            case "FINGERS_SPREAD":
                receivedPose = Pose.FingersSpread;
                break;
            case "DOUBLE_TAP":
                receivedPose = Pose.DoubleTap;
                break;
            case "UNKNOWN":
                receivedPose = Pose.Unknown;
                break;
            default:
                break;
        }

        //receivedPose = (Pose)System.Enum.Parse(typeof(Pose), message, true);
        Debug.Log("Gefiltert Pose: " + receivedPose);
        /*for (int i = (int)Pose.Rest; i <= (int)Pose.Unknown; i++)
        {
            Pose currPose = (Pose)i;
            if (currPose.ToString().Equals(message))
            {
                receivedPose = currPose;
                Debug.Log("Gefiltert Pose: " + receivedPose);
                break;
            }
        }*/
        //Update all myos for now because we don't have the myo id
        if (ThalmicHub.DidReceivePoseChange != null)
        {
            ThalmicHub.DidReceivePoseChange(null, receivedPose);
            Debug.Log("DidReceive Pose: ");
        }
        ThalmicMyo targetMyo = GetMyo("test");
       targetMyo._myoPose = receivedPose;
        /*foreach (ThalmicMyo myo in ThalmicHub.instance._myos)
        {
            if (ThalmicHub.DidReceivePoseChange != null)
            {
                ThalmicHub.DidReceivePoseChange(null, receivedPose);
                Debug.Log("DidReceive Pose: ");
            }
            myo._myoPose = receivedPose;
            Debug.Log("In der Myo Pose: " + myo._myoPose);
        }*/
    }

    public void OnOrientationData(string message)
    {
        string[] tokens = message.Split(',');
        float x = 0, y = 0, z = 0, w = 0;
        float.TryParse(tokens[0], out x);
        float.TryParse(tokens[1], out y);
        float.TryParse(tokens[2], out z);
        float.TryParse(tokens[3], out w);

        //quaternion = new Quaternion(y, z, -x, -w);
        ThalmicMyo targetMyo = GetMyo("test");
        //targetMyo._myoQuaternion = new Thalmic.Myo.Quaternion(y, z, -x, -w);
        targetMyo._myoQuaternion = new Thalmic.Myo.Quaternion(x, y, -z, -w);
    }
    #endregion

    #region Helper Methods
    ThalmicMyo GetMyo(string myoId)
    {
        //if (ThalmicHub.instance == null || ThalmicHub.instance._myos == null) return null;
        //return ThalmicHub.instance._myos[0];
        if (ThalmicHub.instance == null || ThalmicHub.instance._myos == null) return null;

        if (myoId == null || myoId.Length <= 0) return null;

        foreach (ThalmicMyo myo in ThalmicHub.instance._myos)
        {
            if (myo.identifier.Equals(myoId))
            {
                return myo;
            }
        }

        foreach (ThalmicMyo myo in ThalmicHub.instance._myos)
        {
            if (myo.identifier == null || myo.identifier.Length == 0)
            {
                Debug.Log("test zugewiesen");
                myo.identifier = myoId;
                return myo;
            }
        }

        return null;
    }
    #endregion

#endif
}
