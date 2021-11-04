using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using cloudscribe.Pagination.Models;
using AutoMapper;

namespace WebApplication2.Controllers
{
    public class MissionController : Controller
    {
        private readonly TaskDbContext _db;
        private readonly IMapper _mapper;

        public MissionController(TaskDbContext db, IMapper mapper)
        {
            _db = db;

            _mapper = mapper;
        }



        // GET: TaskController
        public ActionResult Index(string keyword, string sortTitle, int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                //Pagination and Sorting From Data Base

                ViewBag.CurrentSortTitle = sortTitle;
                ViewBag.TitleSortParam = string.IsNullOrEmpty(sortTitle) ? "title_desc" : "";
                ViewBag.StartSortParam = sortTitle == "Date" ? "date_desc" : "Date";
                ViewBag.EndSortParam = sortTitle == "EndDate" ? "Enddate_desc" : "EndDate";

                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var missionCount = _db.Missions.Count();

                var mission = from b in _db.Missions.OrderByDescending(x => x.Id)
                              select b;
                var task = _mapper.Map<IEnumerable<MissionDTO>>(mission);


                //Search Method
                if (!String.IsNullOrEmpty(keyword))
                {
                    task = task.Where(b => b.Title.Contains(keyword) ||
                                                b.Description.Contains(keyword) ||
                                                b.startDate.ToString().Contains(keyword) ||
                                                b.endDate.ToString().Contains(keyword));
                    missionCount = _db.Missions.Count();
                }

                //Sorting Logic
                switch (sortTitle)
                {
                    //Case To Sort By Title
                    case "title_desc":
                        task = task.OrderByDescending(b => b.Title);
                        break;

                    //Case To Sort By Start Date
                    case "Date":
                        task = task.OrderByDescending(b => b.startDate);
                        break;

                    case "date_desc":
                        task = task.OrderBy(b => b.startDate);
                        break;

                    //Case To Sort By End Date
                    case "EndDate":
                        task = task.OrderByDescending(b => b.endDate);
                        break;

                    case "Enddate_desc":
                        task = task.OrderBy(b => b.endDate);
                        break;

                    default:
                        task = task.OrderByDescending(b => b.Id);
                        break;
                }

                task = task.Skip(ExcludeRecords).Take(pageSize);

                var result = new PagedResult<MissionDTO>
                {
                    Data = task.AsQueryable().ToList(),
                    TotalItems = missionCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize

                };

                return View(result);
            }
            catch
            {
                return View();

            }
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            Mission task = _db.Missions.SingleOrDefault(m => m.Id == id);
            return View(_mapper.Map<MissionDTO>(task));
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MissionDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto.startDate <= dto.endDate)
                    {
                        _db.Missions.Add(_mapper.Map<Mission>(dto));
                        _db.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return View(dto);
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            var task = _db.Missions.SingleOrDefault(m => m.Id == id);
            return View(_mapper.Map<MissionDTO>(task));
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MissionDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto.startDate <= dto.endDate)
                    {
                        _db.Missions.Update(_mapper.Map<Mission>(dto));
                        _db.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    return View(dto);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
