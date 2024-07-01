using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArbetsProv.Data;
using ArbetsProv.Models;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace ArbetsProv.Controllers
{
    public class PriceDetailsController : Controller
    {
        private readonly PriceDetailsContext _context;

        public PriceDetailsController(PriceDetailsContext context)
        {
            _context = context;
        }



        // GET: PriceDetails
        public async Task<IActionResult> Index()
        {

            var nlist = new List<PriceDetails>();
           

            var list = await _context.PriceDetails.Where(options =>
            options.CatalogEntryCode.Equals("20866-02") && options.MarketId.Equals("sv")
            ).OrderBy(e => e.ValidFrom).ToListAsync();

            for(int i = 0; i <= list.Count; i++)
            {
             


                if (i == 0)
                {

                    list[i].ValidUntil = list[i + 1].ValidFrom;
                    nlist.Add(list[i].Copy());
                }
                else if (i < list.Count - 1 && list[i].UnitPrice == nlist.Last().UnitPrice)
                {
                    var temp = list[i].Copy();
                    nlist.Last().ValidUntil = list[i].ValidUntil;
                    continue;
                }
                else if ( i < list.Count - 1 && list[i].ValidUntil >= list[i + 1].ValidFrom )
                {
                    
                   var temp = list[i].Copy();

                    temp.ValidUntil = list[i+1].Copy().ValidFrom;
                    nlist.Add(temp);

                }
                else if(i < list.Count - 1 && list[i].ValidUntil < list[i + 1].ValidFrom)

                {

                    if (list[i].ValidUntil != list[i+1].ValidFrom)
                    {
                        var temp = list[0].Copy();
                        temp.ValidFrom = list[i].Copy().ValidUntil.GetValueOrDefault();
                        temp.ValidUntil = list[i + 1].Copy().ValidFrom;
                        nlist.Add(temp);
                    }
                    

                    nlist.Add(list[i].Copy());
                    

                   
                }
                else if (i == list.Count)
                {
                   
                   var datelist = list;
                    datelist.OrderBy(e => e.ValidUntil);
                    
                      for(int j = 0; j < datelist.Count; j++)
                      {
                            if (datelist[j].ValidUntil > nlist.Last().ValidUntil)
                            {

                                if (datelist[j].UnitPrice == nlist.Last().UnitPrice)
                                {
                                    var ddtemp = datelist[j];
                                    nlist.Last().ValidUntil = datelist[j].ValidUntil;

                                    continue;
                                }

                                var dtemp = datelist[j];
                                dtemp.ValidFrom = nlist.Last().ValidUntil.GetValueOrDefault();
                                nlist.Add(dtemp);
                                continue;

                            }
                         

                      }
                    var temp = list[0].Copy();
                    temp.ValidFrom = nlist.Last().ValidUntil.GetValueOrDefault();
                    temp.ValidUntil = null;
                    nlist.Add(temp);
                    continue;
 

                    

                }
                else
                {
                    nlist.Add(list[i].Copy());
                    continue;
                }

                
            }

           

            return  View(nlist.OrderBy(e => e.ValidFrom));

        }

        // GET: PriceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceDetail = await _context.PriceDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceDetail == null)
            {
                return NotFound();
            }

            return View(priceDetail);
        }


        // GET: PriceDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Created,Modified,CatalogEntryCode,MarketId,CurrencyCode,ValidFrom,ValidUntil,UnitPrice")] PriceDetails priceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceDetail);
        }

        // GET: PriceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceDetail = await _context.PriceDetails.FindAsync(id);
            if (priceDetail == null)
            {
                return NotFound();
            }
            return View(priceDetail);
        }

        // POST: PriceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,Modified,CatalogEntryCode,MarketId,CurrencyCode,ValidFrom,ValidUntil,UnitPrice")] PriceDetails priceDetail)
        {
            if (id != priceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceDetailExists(priceDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(priceDetail);
        }

        // GET: PriceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceDetail = await _context.PriceDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceDetail == null)
            {
                return NotFound();
            }

            return View(priceDetail);
        }

        // POST: PriceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priceDetail = await _context.PriceDetails.FindAsync(id);
            if (priceDetail != null)
            {
                _context.PriceDetails.Remove(priceDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceDetailExists(int id)
        {
            return _context.PriceDetails.Any(e => e.Id == id);
        }
    }
}
