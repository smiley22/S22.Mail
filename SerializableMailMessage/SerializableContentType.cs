﻿using System;
using System.Collections.Specialized;
using System.Net.Mime;

namespace S22.Mail {
	[Serializable]
	public class SerializableContentType {
		public static implicit operator ContentType(SerializableContentType contentType) {
			if (contentType == null)
				return null;
			ContentType ct = new ContentType();

			ct.Boundary = contentType.Boundary;
			ct.CharSet = contentType.CharSet;
			ct.MediaType = contentType.MediaType;
			ct.Name = contentType.Name;
			foreach (string k in contentType.Parameters.Keys)
				if(ct.Parameters.ContainsKey(k) == false)
					ct.Parameters.Add(k, contentType.Parameters[k]);
			return ct;
		}

		public static implicit operator SerializableContentType(ContentType contentType) {
			if (contentType == null)
				return null;
			return new SerializableContentType(contentType);
		}

		private SerializableContentType(ContentType contentType) {
			Boundary = contentType.Boundary;
			CharSet = contentType.CharSet;
			MediaType = contentType.MediaType;
			Name = contentType.Name;
			Parameters = new StringDictionary();
			foreach (string k in contentType.Parameters.Keys) {
				if(Parameters.ContainsKey(k) == false)
					Parameters.Add(k, contentType.Parameters[k]);
			}
		}

		public string Boundary { get; set; }
		public string CharSet { get; set; }
		public string MediaType { get; set; }
		public string Name { get; set; }
		public StringDictionary Parameters { get; private set; }
	}
}
