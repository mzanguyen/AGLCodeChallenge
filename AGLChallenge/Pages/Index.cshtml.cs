using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGLChallenge.Client.Models;
using AGLChallenge.DTO.DTOModels;
using AGLChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AGLChallenge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPetService _petService;
        public List<PetViewModel> DisplayItem { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IPetService petService)
        {
            _logger = logger;
            _petService = petService;
        }

        public async Task OnGet()
        {
            var petList = await _petService.GetCatByOwnerGender();

            DisplayItem = petList.Select(item => new PetViewModel(item.Key, item.Value?.Select(pet => pet?.Name))).ToList();
        }
    }
}
