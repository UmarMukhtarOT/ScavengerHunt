using UnityEngine;
//using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using System.Collections;
using System.IO;
using UnityEditor;

public class CABuildPostprocessor
{
	internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
	{
		if (Directory.Exists(dstPath))
			Directory.Delete(dstPath);
		if (File.Exists(dstPath))
			File.Delete(dstPath);

		Directory.CreateDirectory(dstPath);

		foreach (var file in Directory.GetFiles(srcPath))
			File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

		foreach (var dir in Directory.GetDirectories(srcPath))
			CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
	}

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
	{

		//if (buildTarget == BuildTarget.iOS)
		//{
		//	string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

		//	PBXProject proj = new PBXProject();
		//	proj.ReadFromString(File.ReadAllText(projPath));

		//	string target = proj.TargetGuidByName("Unity-iPhone");

		//	// Set a custom link flag
		//	proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");

		//	File.WriteAllText(projPath, proj.WriteToString());
		//}
	}
}
