using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_1_Depi.Data;
using MVC_1_Depi.Interfaces;
using MVC_1_Depi.Models;
using MVC_1_Depi.Services;
using MVC_1_Depi.ViewModels;
using System.Runtime.CompilerServices;

namespace MVC_1_Depi.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepo _clubRepo;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepo clubRepository, IPhotoService photoService)
        {
            _clubRepo = clubRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepo.GetAllAsync();
            return View(clubs);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var club = await _clubRepo.GetByIdAsync(id);
            return View(club);
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    ClubCategory = clubVM.ClubCategory,
                    Image = result.Url.ToString(),
                    //Address = clubVM.Address
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street
                    }
                };
                await _clubRepo.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepo.GetByIdAsync(id);
            if (club == null) return View("Error");

            var ClubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory,
            };
            return View(ClubVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepo.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch
                {
                    return View(clubVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    Address = clubVM.Address,
                    AddressId = clubVM.AddressId
                };

                _clubRepo.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }
    }
}
