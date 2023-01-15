using HallGest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallGest.Controllers
{
    public class BoardController : Controller
    {
        // GET: Board
        public ActionResult GetBoards()
        {
            List<Board> boardList = new List<Board>();
            try
            {
                boardList = Board.GetAllBoards();
            }catch(Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View(boardList);
        }
        
        // GET: Board/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Board/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Board/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Board/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Board/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
