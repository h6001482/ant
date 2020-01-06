using System.Data;
using HaeSQLHelper;
using HaeDTO;
using System;

namespace StickerWPF.Biz
{
    public class StickerBiz
    {
        private SQLiteAccess dao;

        private string getStickerIdSQL = @"SELECT COUNT(*) + 1 id FROM sticker";
        private string getStickerListSQL = @"SELECT id, title, contents, x, y, width, height, prev_width, prev_height, opacity, fold_yn, fix_yn, showedit_yn, del_yn, del_date, reg_date FROM sticker";
        private string getStickerByIdSQL = @"SELECT id, title, contents, contents_rtf, x, y, width, height, prev_width, prev_height, opacity, fold_yn, fix_yn, showedit_yn, del_yn, del_date, reg_date FROM sticker WHERE id = @id";
        private string getStickerBySearchTextSQL = @"SELECT id, title, contents, x, y, width, height, prev_width, prev_height, opacity, fold_yn, fix_yn, showedit_yn, del_yn, deldate, reg_date FROM sticker WHERE (title LIKE '%' || @searchText || '%' or contents LIKE '%' || @searchText || '%')";
        private string insStickerSQL = @"INSERT INTO sticker ( id, title, contents, x, y, width, height, prev_width, prev_height, opacity, fold_yn, fix_yn, showedit_yn, reg_date )VALUES( @id, @title, @contents, @x, @y, @width, @height, @prev_width, @prev_height, @opacity, @fold_yn, @fix_yn, @showedit_yn, @reg_date)";
        private string updStickerSQL = @"UPDATE sticker SET title = @title, contents = @contents, contents_rtf=@contents_rtf, x = @x, y = @y, width = @width, height = @height, prev_width = @prev_width, prev_height = @prev_height, opacity = @opacity, fold_yn = @fold_yn, fix_yn = @fix_yn, showedit_yn = @showedit_yn WHERE id = @id";
        private string delStickerSQL = @"UPDATE sticker SET SET del_yn = 'Y' WHERE id = @id";
        //private string truncateStickerSQL = @"DELETE FROM sticker WHERE id = @id";

        public StickerBiz()
        {
        }

        public StickerBiz(string appPath)
        {
            dao = new SQLiteAccess(appPath);
        }

        public DataTable getStickerInfo()
        {            
            return dao.select(getStickerListSQL);
        }

        public DataTable getStickerInfoById(CommonDTO dto)
        {
            return dao.select(getStickerByIdSQL, dto);
        }

        public DataTable getStickerInfoBySearchText(CommonDTO dto)
        {
            return dao.select(getStickerBySearchTextSQL, dto);
        }

        public string insStickerInfo(CommonDTO dto)
        {
            DataTable dt = dao.select(getStickerIdSQL);
            string id = string.Empty;
            if(dt.Rows.Count > 0)
            {
                id = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + dt.Rows[0]["id"].ToString();                
            }
            else
            {
                id = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + "0";
            }
            dto.set("id", id);
            return (dao.execute(insStickerSQL, dto) > 0) ? id : "";
        }

        public bool updStickerInfo(CommonDTO dto)
        {
            return (dao.execute(updStickerSQL, dto) > 0) ? true : false;
        }

        public bool delStickerInfo(CommonDTO dto)
        {
            return (dao.execute(delStickerSQL, dto) > 0) ? true : false;
        }
    }
}
