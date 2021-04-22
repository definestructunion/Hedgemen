using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hgm.Engine.IO
{
	public delegate bool FileListFilter(FileHandle fileHandle);

	public delegate bool DirectoryListFilter(DirectoryHandle directory);
	
	public class DirectoryHandle
	{
		private DirectoryInfo Info { get; set; }
		public FsType Type { get; protected set; }
		
		public DirectoryHandle(string path, FsType type = FsType.Local)
		{
			string actualPath = GetPath(path, type);
			Init(new DirectoryInfo(actualPath), type);
		}

		internal DirectoryHandle(DirectoryInfo info, FsType type)
		{
			Init(info, type);
		}

		private string GetPath(string path, FsType type)
		{
			return path;
		}

		private void Init(DirectoryInfo info, FsType type)
		{
			Info = info;
			Type = type;
		}

		public bool Exists => Info.Exists;

		public string Name => Info.Name;

		public DirectoryHandle Parent => (Info.Parent == null) ? null : new DirectoryHandle(Info.Parent, Type);
		
		public DirectoryHandle Root => new DirectoryHandle(Info.Root, FsType.Absolute);

		public FileAttributes Attributes => Info.Attributes;

		public string Extension => Info.Extension;

		public DateTime CreationTime => Info.CreationTime;

		public string FullName => Info.FullName;

		public DateTime CreationTimeUtc => Info.CreationTimeUtc;

		public DateTime LastAccessTime => Info.LastAccessTime;

		public DateTime LastWriteTime => Info.LastWriteTime;
		
		public DateTime LastAccessTimeUtc => Info.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => Info.LastWriteTimeUtc;

		public void Create()
		{
			Info.Create();
		}

		public DirectoryHandle CreateSubDirectory(string name, bool createIt = true)
		{
			var directory = new DirectoryHandle(Info.CreateSubdirectory(name), Type);
			if(createIt) directory.Create();
			return directory;
		}

		public void CreateSubDirectories(params string[] names)
		{
			foreach (var directoryName in names)
			{
				var directory = CreateSubDirectory(directoryName, true);
			}
		}

		public FileHandle CreateFile(string name, bool createIt = true)
		{
			var file = new FileHandle(name, Type);
			if(createIt) file.Create();
			return file;
		}

		public void Delete(bool recursive = true)
		{
			if(Exists)
				Info.Delete(recursive);
		}

		public void DeleteContents()
		{
			if (Exists)
			{
				Info.GetFiles().ToList().ForEach(e => e.Delete());
				Info.GetDirectories().ToList().ForEach(e => e.Delete(true));
			}
		}

		public void Refresh()
		{
			Info.Refresh();
		}

		public FileHandle FindFile(string fileName)
		{
			return new FileHandle(FullName + '/' + fileName, Type);
		}

		public List<FileHandle> FindFiles(params string[] fileNames)
		{
			var files = new List<FileHandle>(fileNames.Length);
			
			foreach (string fileName in fileNames)
			{
				files.Add(FindFile(fileName));
			}

			return files;
		}

		public DirectoryHandle FindDirectory(string directoryName)
		{
			return new DirectoryHandle(FullName + '/' + directoryName + '/', Type);
		}

		public List<DirectoryHandle> FindDirectories(params string[] directoryNames)
		{
			var directories = new List<DirectoryHandle>(directoryNames.Length);
			
			foreach (string directoryName in directoryNames)
			{
				directories.Add(FindDirectory(directoryName));
			}

			return directories;
		}

		public List<DirectoryHandle> ListDirectories(DirectoryListFilter filter = null)
		{
			filter ??= e => true;

			var directoriesArray = Info.GetDirectories();
			var directoriesList = new List<DirectoryHandle>();
			
			foreach (var directory in directoriesArray)
			{
				var directoryHandle = new DirectoryHandle(directory, Type);
				if(filter(directoryHandle)) directoriesList.Add(directoryHandle);
			}

			return directoriesList;
		}

		public List<FileHandle> ListFiles()
		{
			var files = Info.GetFiles();
			return files.Select(file => new FileHandle(file, Type)).ToList();
		}
		
		public List<FileHandle> ListFilesRecursively(FileListFilter filter = null)
		{
			filter ??= file => true;
			List<FileHandle> files = new List<FileHandle>();
			
			InternalListFilesRecursively(filter, this, files);
			
			return files;
		}

		private void InternalListFilesRecursively(FileListFilter filter, DirectoryHandle directory, List<FileHandle> files)
		{
			foreach(var file in directory.ListFiles())
			{
				if(filter(file)) files.Add(file);
			}

			foreach (var dir in directory.ListDirectories())
			{
				InternalListFilesRecursively(filter, dir, files);
			}
		}

		public List<FileSystemInfo> ListFileSystems()
		{
			var systems = Info.GetFileSystemInfos();
			return systems.ToList();
		}

		public void MoveTo(DirectoryHandle dest)
		{
			Info.MoveTo(dest.FullName);
		}

		public void CopyTo(DirectoryHandle dest)
		{
			dest.Delete(true);
			Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(FullName, dest.FullName);
		}
	}
}