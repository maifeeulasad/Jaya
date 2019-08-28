﻿using Avalonia.Data.Converters;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    public class FileSystemObjectToFileNameConverter : IValueConverter
    {
        readonly ConfigurationService _configService;

        public FileSystemObjectToFileNameConverter()
        {
            _configService = ServiceLocator.Instance.GetService<ConfigurationService>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fso = value as FileSystemObjectModel;
            if (fso == null)
                return null;

            switch (fso.Type)
            {
                case FileSystemObjectType.Drive:
                case FileSystemObjectType.Directory:
                    return fso.Name;

                case FileSystemObjectType.File:
                    var file = fso as FileModel;
                    if (_configService.ApplicationConfiguration.IsFileNameExtensionVisible)
                        return string.Format("{0}.{1}", file.Name, file.Extension);
                    else
                        return file.Name;

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
