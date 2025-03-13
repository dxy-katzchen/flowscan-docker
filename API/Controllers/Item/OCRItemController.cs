
using API.Models.Response;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Item
{

    public class OCRItemController : BaseAuthApiController
    {
        private readonly OCRItemService _ocrItemService;

        public OCRItemController(OCRItemService ocrItemService)
        {
            _ocrItemService = ocrItemService;
        }

        /// <summary>
        /// Get all OCR items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<List<Entities.OCRItem>>), 200)]
        public async Task<ActionResult<SuccessResponse<List<Entities.OCRItem>>>> GetOCRItems()
        {
            var ocrItems = await _ocrItemService.GetOCRItemsAsync();
            var result = new SuccessResponse<List<Entities.OCRItem>>(ocrItems);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<Entities.OCRItem>), 200)]
        public async Task<ActionResult<SuccessResponse<Entities.OCRItem>>> GetOCRItem(int id)
        {
            var ocrItem = await _ocrItemService.GetOCRItemAsync(id);
            var result = new SuccessResponse<Entities.OCRItem>(ocrItem);
            return Ok(result);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult<SuccessResponse<string>>> DeleteOCRItem(int id)
        {
            await _ocrItemService.DeleteOCRItemAsync(id);
            var result = new SuccessResponse<string>("OCR item deleted successfully");
            return Ok(result);
        }


    }
}