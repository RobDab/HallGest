using HallGest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallGest.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult AllReservations()
        {
            List<Reservation> list = new List<Reservation>();
            try
            {
                list = Reservation.AllReservations();
            }catch(Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }

            return View(list);
        }

        // GET: Reservation/Create
        public ActionResult CreateReservation(int id)
        {
            // Codice per reperire lista dei BoardTypes
            List<SelectListItem> BoardDDLItems = new List<SelectListItem>();  
            try
            {
                List<Board> boardList = Board.GetAllBoards();
                foreach(Board b in boardList)
                {
                    SelectListItem board = new SelectListItem();
                    board.Text = b.BoardType;
                    board.Value = b.BoardTypeID.ToString();
                    BoardDDLItems.Add(board);
                }
                ViewBag.BoardList = BoardDDLItems;
               
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        public ActionResult CreateReservation(Reservation current, int customerID)
        {
            //Customer currentCustomer = Customer.GetByID(customerID);

            try
            {
                Reservation.AddReservation(current, customerID);

                return RedirectToAction("Home", "Home");
            }
            catch(Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                return View();
            }
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reservation/Edit/5
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

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservation/Delete/5
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
