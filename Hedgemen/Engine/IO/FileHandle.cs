using System;
using System.IO;
using System.Text;

namespace Hgm.Engine.IO
{
	public class FileHandle
	{
		private readonly FileInfo info;
		public FsType Type { get; protected set; }
		
		public FileAccess Access { get; set; }
		public FileShare Share { get; set; }
		public Encoding Encoding { get; set; } = Encoding.Default;
		
		public FileHandle(string name, FsType type = FsType.Local)
		{
			RelativePath = name;
			string actualName = GetPath(name, type);
			info = new FileInfo(actualName);
			Init(type);
		}

		internal FileHandle(FileInfo info, FsType type)
		{
			this.info = info;
			Init(type);
		}

		private void Init(FsType type)
		{
			Type = type;
			Access = FileAccess.ReadWrite;
			Share = FileShare.ReadWrite;
			Encoding = Encoding.UTF8;
		}
		
		private string GetPath(string path, FsType type)
		{
			return path;
		}

		public bool Exists => info.Exists;

		public DirectoryHandle Directory => new DirectoryHandle(info.Directory, Type);

		public long Length => info.Length;

		public string Name => info.Name;

		public string DirectoryName => Directory.Name;

		public bool IsReadOnly => info.IsReadOnly;

		public FileAttributes Attributes => info.Attributes;

		public string Extension => info.Extension;

		public DateTime CreationTime => info.CreationTime;

		public string FullName => info.FullName;

		public string RelativePath { get; protected set; }

		public DateTime CreationTimeUtc => info.CreationTimeUtc;

		public DateTime LastAccessTime => info.LastAccessTime;

		public DateTime LastWriteTime => info.LastWriteTime;
		
		public DateTime LastAccessTimeUtc => info.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => info.LastWriteTimeUtc;

		public void Create()
		{
			if (Exists) return;
			Directory.Create();
			info.Create().Close();
		}

		public void Delete()
		{
			info.Delete();
		}
		
		public virtual Stream Open(FileMode mode = FileMode.Open)
		{
			return info.Open(mode, Access, Share);
		}

		public void WriteString(string text)
		{
			EnsureCreated();
			using StreamWriter writer = new StreamWriter(Open(FileMode.Truncate), Encoding);
			writer.Write(text);
		}
		
		public void WriteBytes(byte[] buffer)
		{
			WriteBytes(buffer, 0, buffer.Length);
		}
		
		public void WriteBytes(byte[] buffer, int index, int count, FileMode fileMode = FileMode.Truncate)
		{
			EnsureCreated();
			using BinaryWriter writer = new BinaryWriter(Open(fileMode), Encoding);
			writer.Write(buffer, index, count);
		}

		public string ReadString(FileMode fileMode = FileMode.Open)
		{
			if (!Exists) return string.Empty;
			using StreamReader reader = new StreamReader(Open(fileMode), Encoding);
			return reader.ReadToEnd();
		}

		public byte[] ReadBytes(FileMode fileMode = FileMode.Open)
		{
			using var stream = Open(fileMode);
			using var ms = new MemoryStream();
			
			stream.CopyTo(ms);
			return ms.ToArray();
		}

		public void CopyTo(FileHandle dest)
		{
			dest.Directory.Create();
			info.CopyTo(dest.FullName);
		}

		public void MoveTo(FileHandle dest)
		{
			info.MoveTo(dest.FullName);
		}
		
		public FileHandle CopyTo(DirectoryHandle dest, string name)
		{
			var file = new FileHandle(dest.FullName + '/' + name, dest.Type);
			CopyTo(file);
			return file;
		}

		public FileHandle MoveTo(DirectoryHandle dest, string name)
		{
			var file = new FileHandle(dest.FullName + '/' + name, dest.Type);
			MoveTo(file);
			return file;
		}

		public override string ToString()
		{
			return FullName;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			var objVal = (FileHandle) obj;
			return Type == objVal.Type && FullName.Equals(objVal.FullName);
		}

		public override int GetHashCode()
		{
			int hash = 1;
			hash = hash * 37 + info.GetHashCode();
			hash = hash * 67 + FullName.GetHashCode();
			return hash;
		}

		private void EnsureCreated()
		{
			Create();
		}
	}
}