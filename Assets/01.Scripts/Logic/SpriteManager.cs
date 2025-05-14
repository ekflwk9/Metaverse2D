using System.Collections.Generic;
using UnityEngine;

public class SpriteManager
{
    public Dictionary<string, Sprite> image = new Dictionary<string, Sprite>();

    public void Load()
    {
        var imageFile = Resources.LoadAll<Sprite>("Image");

        for (int i = 0; i < imageFile.Length; i++)
        {
            if (!image.ContainsKey(imageFile[i].name))
            {
                image.Add(imageFile[i].name, imageFile[i]);
            }
        }
    }

    /// <summary>
    /// ��ȯ�� �̹����� �̸�
    /// </summary>
    /// <param name="_imageName"></param>
    /// <returns></returns>
    public Sprite GetImage(string _imageName)
    {
        if (image.ContainsKey(_imageName))
        {
            Service.Log($"{_imageName}�� Image���Ͽ� �������� �ʴ� �̹�����");
            return default;
        }

        return image[_imageName];
    }
}
