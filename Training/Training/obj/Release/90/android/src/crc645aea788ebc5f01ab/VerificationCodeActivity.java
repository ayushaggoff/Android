package crc645aea788ebc5f01ab;


public class VerificationCodeActivity
	extends crc645aea788ebc5f01ab.LoginActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Training.Activity.VerificationCodeActivity, Training", VerificationCodeActivity.class, __md_methods);
	}


	public VerificationCodeActivity ()
	{
		super ();
		if (getClass () == VerificationCodeActivity.class)
			mono.android.TypeManager.Activate ("Training.Activity.VerificationCodeActivity, Training", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
