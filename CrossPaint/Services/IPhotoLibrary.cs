using System;
using System.IO;
using System.Threading.Tasks;

namespace CrossPaint.Services
{
    public interface IPhotoLibrary
    {
        Task<Stream> PickPhotoAsync();

        Task<bool> SavePhotoAsync(byte[] data, string folder, string filename);
    }
}
