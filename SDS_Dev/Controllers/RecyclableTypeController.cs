using SDS_Dev.Models;
using SDS_Dev.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDS_Dev.Controllers
{
    public class RecyclableTypeController : Controller
    {
        RecyclableTypeRepository _repo = new RecyclableTypeRepository();
        // GET: RecyclableType
        public ActionResult Index()
        {
            var listOfRecyclableTypes = _repo.GetAllRecyclableTypes();
            if (listOfRecyclableTypes.Count == 0)
            {
                TempData["InfoMessage"] = "No Recyclable Types currently available in the database.";
            }
            return View(listOfRecyclableTypes);
        }

        // GET: RecyclableType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecyclableType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecyclableType/Create
        [HttpPost]
        public ActionResult Create(RecyclableType recyclableType)
        {
            try
            {
                bool isInserted = false;
                if (ModelState.IsValid)
                {
                    isInserted = _repo.InsertRecyclableType(recyclableType);
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

        // GET: RecyclableType/Edit/5
        public ActionResult Edit(int id)
        {
            var getRecyclableType = _repo.GetRecyclableTypeById(id).FirstOrDefault();
            if (getRecyclableType == null)
            {
                TempData["InfoMessage"] = "Product with ID #" + id.ToString() + " is not available.";
                return RedirectToAction("Index");
            }
            return View(getRecyclableType);
        }

        // POST: RecyclableType/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateRecyclableType(RecyclableType recyclableType)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool isUpdated = _repo.UpdateRecyclableType(recyclableType);
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Recyclable Type with ID #" + recyclableType.Id.ToString() + " updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update Recyclable Type with ID #" + recyclableType.Id.ToString();
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

        // GET: RecyclableType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var getRecyclableType = _repo.GetRecyclableTypeById(id).FirstOrDefault();
                if (getRecyclableType == null)
                {
                    TempData["InfoMessage"] = "Product with ID #" + id.ToString() + " is not available.";
                    return RedirectToAction("Index");
                }
                return View(getRecyclableType);
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
                string result = _repo.DeleteRecyclableType(id);
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
