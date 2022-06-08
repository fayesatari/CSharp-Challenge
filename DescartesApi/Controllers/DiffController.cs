using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DescartesApi;
using DescartesApi.Models;
using DescartesApi.Data;
using System.Text;
using System.Text.Json;

namespace DescartesApi.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    public class DiffController : Controller
    {
        private readonly DiffContext _context;

        public DiffController(DiffContext context)
        {
            _context = context;
        }

        // GET: v1/diff/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiffItem>>> GetAll()
        {
            if (_context.DiffItems == null) return Problem("Database is empty!");
            return await _context.DiffItems.ToListAsync();
        }

        // GET: v1/diff/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiffResult>> GetById(long id)
        {
            // Check input parameters
            if (id <= 0) return BadRequest("Invalid id");

            // Not Found check
            if (_context.DiffItems == null) return Problem("Database is empty!");
            var diffItem = await _context.DiffItems.FindAsync(id);
            if (diffItem == null) return NotFound();

            // Null check
            if (diffItem.left == null || diffItem.right == null) return NotFound();

            // Equals check
            if (diffItem.left == diffItem.right) return Ok(new DiffResult { diffResultType = DiffResultType.Equals });

            // Length check
            if (diffItem.left.Length != diffItem.right.Length) return Ok(new DiffResult { diffResultType = DiffResultType.SizeDoNotMatch });

            // Diffrentiate right and left 
            var diffResult = new DiffResult
            {
                diffs = new List<DiffedType>(),
                diffResultType = DiffResultType.ContentDoNotMatch
            };
            var left = diffItem.left.ToCharArray();
            var right = diffItem.right.ToCharArray();
            int lastEqualIndex = -1;
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == right[i])
                    lastEqualIndex = i;
                else if (i - lastEqualIndex > 1)
                    diffResult.diffs[diffResult.diffs.Count - 1].length++;
                else
                    diffResult.diffs.Add(new DiffedType { offset = i, length = 1 });
            }
            return Ok(diffResult);
        }

        // PUT: v1/diff/1/left
        // PUT: v1/diff/1/right
        [HttpPut("{id}/{side}")]
        public async Task<IActionResult> PutById(long id, string side, DiffInput diffInput)
        {
            // 1- Check input parameters
            if (side != "left" && side != "right") return NotFound();
            if (diffInput is null || diffInput.data is null) return BadRequest();

            // 2- Decod the input data
            string decodedData;
            try
            {
                decodedData = Encoding.ASCII.GetString(Convert.FromBase64String(diffInput.data));
            }
            catch (Exception e)
            {
                return BadRequest("Invalid data: " + e.Message);
            }

            // 3- Create the diffItem
            if (_context.DiffItems == null) return Problem("Database is empty!");
            var dbDiffItem = await _context.DiffItems.FindAsync(id);
            if (dbDiffItem == null)
            {
                _context.DiffItems.Add(new DiffItem
                {
                    id = id,
                    left = (side == "left") ? decodedData : null,
                    right = (side == "right") ? decodedData : null
                });
                await _context.SaveChangesAsync();
                return new CreatedResult("", null);
            }

            // 4- Update the diffItem
            else
            {
                if (side == "left") dbDiffItem.left = decodedData;
                else if (side == "right") dbDiffItem.right = decodedData;
                await _context.SaveChangesAsync();
                return new CreatedResult("", null);
            }
        }

        //private bool DiffItemExists(long id) => (_context.DiffItems?.Any(e => e.id == id)).GetValueOrDefault();



























        //// GET: Diff
        //public async Task<IActionResult> Index()
        //{
        //      return _context.DiffItem != null ? 
        //                  View(await _context.DiffItem.ToListAsync()) :
        //                  Problem("Entity set 'DiffContext.DiffItem'  is null.");
        //}

        //// GET: Diff/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null || _context.DiffItem == null)
        //    {
        //        return NotFound();
        //    }

        //    var diffItem = await _context.DiffItem
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (diffItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(diffItem);
        //}

        //// GET: Diff/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Diff/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,left,right")] DiffItem diffItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(diffItem);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(diffItem);
        //}

        //// GET: Diff/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null || _context.DiffItem == null)
        //    {
        //        return NotFound();
        //    }

        //    var diffItem = await _context.DiffItem.FindAsync(id);
        //    if (diffItem == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(diffItem);
        //}

        //// POST: Diff/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long? id, [Bind("id,left,right")] DiffItem diffItem)
        //{
        //    if (id != diffItem.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(diffItem);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DiffItemExists(diffItem.id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(diffItem);
        //}

        //// GET: Diff/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null || _context.DiffItem == null)
        //    {
        //        return NotFound();
        //    }

        //    var diffItem = await _context.DiffItem
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (diffItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(diffItem);
        //}

        //// POST: Diff/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long? id)
        //{
        //    if (_context.DiffItem == null)
        //    {
        //        return Problem("Entity set 'DiffContext.DiffItem'  is null.");
        //    }
        //    var diffItem = await _context.DiffItem.FindAsync(id);
        //    if (diffItem != null)
        //    {
        //        _context.DiffItem.Remove(diffItem);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool DiffItemExists(long? id)
        //{
        //  return (_context.DiffItem?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
