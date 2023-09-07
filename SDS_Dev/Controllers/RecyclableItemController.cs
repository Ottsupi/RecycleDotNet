using SDS_Dev.Models;
using SDS_Dev.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDS_Dev.Controllers
{
    public class RecyclableItemController : Controller
    {
        RecyclableItemRepository _repo = new RecyclableItemRepository();
        RecyclableTypeRepository _repoType = new RecyclableTypeRepository();

        // GET: RecyclableItem
        public ActionResult Index()
        {
            var listOfRecyclableItems = _repo.GetAllRecyclableItems();
            if (listOfRecyclableItems.Count == 0)
            {
                TempData["InfoMessage"] = "No Recyclable Types currently available in the database.";
            }
            return View(listOfRecyclableItems);
        }

        // GET: RecyclableItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecyclableItem/Create
        public ActionResult Create()
        {
            List<RecyclableTypeOptions> options = PopulateOptions();
            ViewData["RecyclableTypeOptions"] = options;
            return View();
        }

        private List<RecyclableTypeOptions>  PopulateOptions()
        {
            List<RecyclableTypeOptions> options = _repoType.GetAllRecyclableTypesOptions();
            return options;
        }

        // POST: RecyclableItem/Create
        [HttpPost]
        public ActionResult Create(RecyclableItem recyclableItem)
        {
            try
            {
                bool isInserted = false;
                if (ModelState.IsValid)
                {
                    isInserted = _repo.InsertRecyclableItem(recyclableItem);
                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "New Recyclable Type created successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to create the Recyclable Type";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: RecyclableItem/Edit/5
        public ActionResult Edit(int id)
        {
            var getRecyclableItem = _repo.GetRecyclableItemById(id).FirstOrDefault();
            if (getRecyclableItem == null)
            {
                TempData["InfoMessage"] = "Product with ID #" + id.ToString() + " is not available.";
                return RedirectToAction("Index");
            }
            return View(getRecyclableItem);
        }

        // POST: RecyclableItem/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateRecyclableItem(RecyclableItem recyclableItem)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool isUpdated = _repo.UpdateRecyclableItem(recyclableItem);
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Recyclable Type with ID #" + recyclableItem.Id.ToString() + " updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update Recyclable Type with ID #" + recyclableItem.Id.ToString();
                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: RecyclableItem/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var getRecyclableItem = _repo.GetRecyclableItemById(id).FirstOrDefault();
                if (getRecyclableItem == null)
                {
                    TempData["InfoMessage"] = "Product with ID #" + id.ToString() + " is not available.";
                    return RedirectToAction("Index");
                }
                return View(getRecyclableItem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: RecyclableType/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = _repo.DeleteRecyclableItem(id);
                TempData["SuccessMessage"] = result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
