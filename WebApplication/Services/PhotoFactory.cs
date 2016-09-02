using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using photoResizer;
using WebApplication.Models;

namespace WebApplication.Services
{
    public static class PhotoFactory
    {
        public static PhotoManager GetManager(ConfigModel model)
        {
//            AppDomain domain = AppDomain.CreateDomain("Photo Resizer");

            //            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
//            string assemblyName = typeof(PhotoManager).Assembly.FullName;
//            string typeName = typeof(PhotoManager).FullName;

            var config = new ConfigManager(model.OutputFolder, model.InputFolder, model.ThreadCount, model.ThreadCapacity, model.Resolutions);
//            var photoManager = domain.CreateInstanceAndUnwrap(assemblyName, typeName) as PhotoManager;
            var photoManager = new PhotoManager();
            photoManager?.SetConfig(config);

            return photoManager;
        }
    }
}