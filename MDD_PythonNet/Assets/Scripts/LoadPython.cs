using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using System.IO;
using System;

public class LoadPython : MonoBehaviour
{
    [Header("directory of .py file")]
    private string python_file_path = @"D:\GithubProject\MDD_PythonNet\Assets\PythonScripts\sample.py";
    private string python_file_path2 = @"D:\GithubProject\MDD_PythonNet\Assets\PythonScripts\twosample.py";
    
    private PythonEngine _pythonEngine;

    private void Start()
    {
        PythonEngine.Initialize();

        using (Py.GIL())
        {
            try
            {
                dynamic os = Py.Import("os"); // python에서의 'os'모듈을 import 하는 방법
                dynamic sys = Py.Import("sys"); // python에서의 'sys'모듈을 import 하는 방법

                // 실행할 Python 파일 경로
                sys.path.append(os.path.dirname(os.path.expanduser(python_file_path)));// 파이썬 파일이 저장된 폴더까지의 경로를 반환
                PyObject fromFile = Py.Import(Path.GetFileNameWithoutExtension(python_file_path)); // 확장자를 제외한 파일 경로를 반환
                PyObject fromFile2 = Py.Import(Path.GetFileNameWithoutExtension(python_file_path2));

                PyObject pyObj = new PyInt(5);

                PyObject[] pyObjects = new PyObject[2];
                pyObjects[0] = new PyInt(1); // Pyint의 부모의 부모가 PyObject
                pyObjects[1] = new PyInt(3);

                PyObject a = fromFile.InvokeMethod("a");
                Debug.Log(int.Parse(a.ToString()) + int.Parse(fromFile.InvokeMethod("b", pyObj).ToString()));

                Debug.Log(fromFile.InvokeMethod("a"));
                Debug.Log(fromFile.InvokeMethod("b", pyObj));
                Debug.Log(fromFile.InvokeMethod("c", pyObjects));

                sys.path.append(os.path.dirname(os.path.expanduser(python_file_path2)));

                Debug.Log(fromFile2.InvokeMethod("two_a"));
                Debug.Log(fromFile.InvokeMethod("c", pyObjects));

                Debug.Log("모두 성공적인 호출");
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError(ex.Message);
            }
        }
    }

    private void InitPythonFile()
    {

    }

    private void CallPythonFile()
    {

    }

    public void CallFunc()
    {
        using(Py.GIL())
        {
            try
            {
                dynamic os = Py.Import("os"); // python에서의 'os'모듈을 import 하는 방법
                dynamic sys = Py.Import("sys"); // 위와 동일

                // 실행할 Python 파일 경로
                sys.path.append(os.path.dirname(os.path.expanduser(python_file_path)));// 파이썬 파일이 저장된 폴더까지의 경로를 반환
                PyObject fromFile = Py.Import(Path.GetFileNameWithoutExtension(python_file_path)); // 확장자를 제외한 파일 경로를 반환

                Debug.Log(fromFile.InvokeMethod("a"));
            }
            catch(Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError(ex.Message);
            }
        }
    }
}
