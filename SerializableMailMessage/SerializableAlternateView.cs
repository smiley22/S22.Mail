﻿using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace S22.Mail {
	[Serializable]
	public class SerializableAlternateView {
		public static implicit operator AlternateView(SerializableAlternateView view) {
			if (view == null)
				return null;
			AlternateView v = new AlternateView(view.ContentStream);

			v.BaseUri = view.BaseUri;
			foreach (SerializableLinkedResource res in view.LinkedResources)
				v.LinkedResources.Add(res);
			v.ContentId = view.ContentId;
			v.ContentType = view.ContentType;
			v.TransferEncoding = view.TransferEncoding;
			return v;
		}

		public static implicit operator SerializableAlternateView(AlternateView view) {
			if (view == null)
				return null;
			return new SerializableAlternateView(view);
		}

		private SerializableAlternateView(AlternateView view) {
			BaseUri = view.BaseUri;
			LinkedResources = new SerializableLinkedResourceCollection();
			foreach (LinkedResource res in view.LinkedResources)
				LinkedResources.Add(res);
			ContentId = view.ContentId;
			ContentStream = new MemoryStream();
			view.ContentStream.CopyTo(ContentStream);
			view.ContentStream.Position = 0;
			ContentStream.Position = 0;
			ContentType = view.ContentType;
			TransferEncoding = view.TransferEncoding;
		}

		public Uri BaseUri { get; set; }
		public SerializableLinkedResourceCollection LinkedResources { get; private set; }
		public string ContentId { get; set; }
		public Stream ContentStream { get; private set; }
		public SerializableContentType ContentType { get; set; }
		public TransferEncoding TransferEncoding { get; set; }
	}
}
