using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLon_ThienDao.Models;

namespace BTLon_ThienDao.Controllers
{
    public class BaiDangController : BaseController
    {
        private readonly QLBTLViecLamDBContext _context;

        public BaiDangController(QLBTLViecLamDBContext context)
        {
            _context = context;
        }

        // GET: BaiDang
        public async Task<IActionResult> Search(string txtSearch)
        {
            var QLBTLViecLamDBContext = _context.BaiDangs.Where(m => m.CongTy.Contains(txtSearch) || m.NoiDung.Contains(txtSearch)).Select(m =>
            new BaiDang()
            {
                BaiID = m.BaiID,
                CongTy = m.CongTy,
                NgayDang = m.NgayDang,
                MucLuong = m.MucLuong,
                NoiDung = m.NoiDung,
                SoLuongBaiDang = m.DanhSachBaiDang.Count()
            });
            return View(nameof(Index), await QLBTLViecLamDBContext.ToListAsync());
        }
        // GET: BaiDang
        public async Task<IActionResult> Index()
        {
            var contactRecords = await _context.BaiDangs
                .Select(c => new BaiDang
                {
                    BaiID = c.BaiID,
                    AccID = c.AccID,
                    CongTy = c.CongTy,
                    NgayDang = c.NgayDang,
                    MucLuong = c.MucLuong,
                    NoiDung = c.NoiDung
                }).ToListAsync();
            return View(contactRecords);
        }
        // GET: BaiDang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BaiID == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // GET: BaiDang/Create
        public IActionResult Create()
        {
            ViewData["AccID"] = new SelectList(_context.Accounts, "AccID", "Username");
            return View();
        }

        // POST: BaiDang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BaiID,AccID,CongTy,NgayDang,MucLuong,NoiDung")] BaiDang baiDang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiDang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccID"] = new SelectList(_context.Accounts, "AccID", "Username", baiDang.AccID);
            return View(baiDang);
        }

        // GET: BaiDang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs.FindAsync(id);
            if (baiDang == null)
            {
                return NotFound();
            }
            ViewData["AccID"] = new SelectList(_context.Accounts, "AccID", "Password", baiDang.AccID);
            return View(baiDang);
        }

        // POST: BaiDang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BaiID,AccID,CongTy,NgayDang,MucLuong,NoiDung")] BaiDang baiDang)
        {
            if (id != baiDang.BaiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiDang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiDangExists(baiDang.BaiID))
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
            ViewData["AccID"] = new SelectList(_context.Accounts, "AccID", "Password", baiDang.AccID);
            return View(baiDang);
        }

        // GET: BaiDang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.BaiDangs
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BaiID == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // POST: BaiDang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaiDangs == null)
            {
                return Problem("Entity set 'QLBTLViecLamDBContext.BaiDangs'  is null.");
            }
            var baiDang = await _context.BaiDangs.FindAsync(id);
            if (baiDang != null)
            {
                _context.BaiDangs.Remove(baiDang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiDangExists(int id)
        {
          return (_context.BaiDangs?.Any(e => e.BaiID == id)).GetValueOrDefault();
        }
    }
}
