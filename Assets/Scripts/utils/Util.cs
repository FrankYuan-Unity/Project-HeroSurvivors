using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util  
{
  public static bool isMobile() {

#if UNITY_ANDROID
        return true;
#elif UNITY_IOS
    return true;
#else
  return false;
#endif

    }
}
