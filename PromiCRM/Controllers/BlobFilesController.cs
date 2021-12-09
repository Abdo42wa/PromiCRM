using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromiCRM.Models;
using PromiCRM.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobFilesController : ControllerBase
    {
        public readonly IBlobService _blobService;
        public BlobFilesController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var files = await _blobService.AllBlobs("productscontainer");
            return Ok(files);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetFile(string name)
        {
            //getting url of image in productscontainer by specified name. returning url
            var res = await _blobService.GetBlob(name, "productscontainer");
            return Ok(res);
        }
        /// <summary>
        /// IFormFile is file type that view will be able to send
        /// us so we can receive it
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file == null || file.Length < 1)
                return BadRequest();
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            //call blob service and insert it
            var res = await _blobService.UploadBlob(fileName, file, "productscontainer");
            if (res != null)
            {
                return Ok(res);
            }

            return Ok(res);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteFile(string name)
        {
            await _blobService.DeleteBlob(name, "productscontainer");
            return RedirectToAction("Index");
            /*return Ok(name);*/
        }


    }
}
