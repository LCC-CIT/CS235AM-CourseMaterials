using System;
using Android.OS;
using System.Collections.Generic;

namespace VocabPractice
{
	public class VocabItem
	{
		public string Spanish { get; set; }
		public string English { get; set; }
		public string PartOfSpeech { get; set; }
		public string Category { get; set; }

		public VocabItem(string s, string e)
		{
			Spanish = s;
			English = e;
		}

		public VocabItem(string cat, string s, string e, string pos)
		{
			Category = cat;
			Spanish = s;
			English = e;
			PartOfSpeech = pos;

		}

		public override string ToString ()
		{
			return Spanish;
		}
	}


	public class VocabItems : Java.Lang.Object, IParcelable
	{
		public List<VocabItem> Items { get; set; }

		public void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
		{
			dest.WriteList(Items);
		}

		public int DescribeContents ()
		{
			return 0;
		}
	}
}

/*
 *  public class Credentials : Object, IParcelable
    {
        [ExportField ("CREATOR")]
        static CredentialsCreator InititalizeCreator()
        {
            return new CredentialsCreator ();
        }

        public String Username { get; set; }
        public String Password { get; set; }

        public Credentials (String username, String password)
        {
            Username = username;
            Password = password;
        }


        public void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
        {
            dest.WriteString (Username);
            dest.WriteString (Password);
        }

        public int DescribeContents ()
        {
            return 0;
        }

    }

    public class CredentialsCreator: Object, IParcelableCreator
    {
        public Object CreateFromParcel (Parcel source)
        {
            return new Credentials (source.ReadString (), source.ReadString ());
        }

        public Object [] NewArray(int size)
        {
            return new Object[size];
        }


    }
    */