using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using SubSonic.Query;

namespace Solution.Logic.Managers {
	/// <summary>
	/// AdvertisingPosition表逻辑类
	/// </summary>
	public partial class AdvertisingPositionBll : LogicBase {
 
 		/***********************************************************************
		 * 模版生成函数                                                        *
		 ***********************************************************************/
		#region 模版生成函数
				
		private const string const_CacheKey = "Cache_AdvertisingPosition";
        private const string const_CacheKey_Date = "Cache_AdvertisingPosition_Date";

		#region 单例模式
		//定义单例实体
		private static AdvertisingPositionBll _AdvertisingPositionBll = null;

		/// <summary>
		/// 获取本逻辑类单例
		/// </summary>
		/// <returns></returns>
		public static AdvertisingPositionBll GetInstence() {
			if (_AdvertisingPositionBll == null) {
				_AdvertisingPositionBll = new AdvertisingPositionBll();
			}
			return _AdvertisingPositionBll;
		}
		#endregion
		
		#region 清空缓存
        /// <summary>清空缓存</summary>
        private void DelAllCache()
        {
            //清除模板缓存
            CacheHelper.RemoveOneCache(const_CacheKey);
			CacheHelper.RemoveOneCache(const_CacheKey_Date);

			//清除前台缓存
			CommonBll.RemoveCache(const_CacheKey);
			//运行自定义缓存清理程序
            DelCache();
        }
		#endregion

		#region IIS缓存函数
		
		#region 从IIS缓存中获取AdvertisingPosition表记录
		/// <summary>
        /// 从IIS缓存中获取AdvertisingPosition表记录
        /// </summary>
	    /// <param name="isCache">是否从缓存中读取</param>
        public IList<DataAccess.Model.AdvertisingPosition> GetList(bool isCache = true)
        {
			try
			{
				//判断是否使用缓存
				if (CommonBll.IsUseCache() && isCache){
					//检查指定缓存是否过期——缓存当天有效，第二天自动清空
					if (CommonBll.CheckCacheIsExpired(const_CacheKey_Date)){		        
						//删除缓存
						DelAllCache();
					}

					//从缓存中获取DataTable
					var obj = CacheHelper.GetCache(const_CacheKey);
					//如果缓存为null，则查询数据库
					if (obj == null)
					{
						var list = GetList(false);

						//将查询出来的数据存储到缓存中
                        CacheHelper.SetCache(const_CacheKey, list);
						//存储当前时间
						CacheHelper.SetCache(const_CacheKey_Date, DateTime.Now);

                        return list;
					}
					//缓存中存在数据，则直接返回
					else
					{
						return (IList<DataAccess.Model.AdvertisingPosition>)obj;
					}
				}
				else
				{
					//定义临时实体集
					IList<DataAccess.Model.AdvertisingPosition> list = null;

					//获取全表缓存加载条件表达式
					var exp = GetExpression<AdvertisingPosition>();
                    //如果条件为空，则查询全表所有记录
					if (exp == null)
					{
						//从数据库中获取所有记录
						var all = AdvertisingPosition.All();
                        list = all == null ? null : Transform(all.ToList());
					}
					else
					{
                        //从数据库中查询出指定条件的记录，并转换为指定实体集
						var all = AdvertisingPosition.Find(exp);
                        list = all == null ? null : Transform(all);
					}

					return list;
				}				
			}
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取AdvertisingPosition表记录时出现异常", e);
			}
            
            return null;
        }
		#endregion

		#region 获取指定Id记录
		/// <summary>
        /// 获取指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
		/// <returns>DataAccess.Model.AdvertisingPosition</returns>
        public DataAccess.Model.AdvertisingPosition GetModel(long id, bool isCache = true)
        {
            //判断是否使用缓存
		    if (CommonBll.IsUseCache() && isCache)
		    {
                //从缓存中获取List
		        var list = GetList();
		        if (list == null || list.Count == 0)
		        {
		            return null;
		        }
		        else
		        {
                    //在List查询指定主键Id的记录
		            return list.SingleOrDefault(x => x.Id == id);
		        }
		    }
		    else
		    {
                //从数据库中直接读取
                var model = AdvertisingPosition.SingleOrDefault(x => x.Id == id);
                if (model == null)
                {
                    return null;
                }
                else
                {
                    //对查询出来的实体进行转换
                    return Transform(model);
                }
		    }
        }
		#endregion

		#region 从IIS缓存中获取指定Id记录
		/// <summary>
        /// 从IIS缓存中获取指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
		/// <returns>DataAccess.Model.AdvertisingPosition</returns>
        public DataAccess.Model.AdvertisingPosition GetModelForCache(long id)
        {
			try
			{
				//从缓存中读取指定Id记录
                var model = GetModelForCache(x => x.Id == id);

				if (model == null){
					//从数据库中读取
					var tem = AdvertisingPosition.SingleOrDefault(x => x.Id == id);
					if (tem == null)
					{
						return null;
					}
					else
					{
						//对查询出来的实体进行转换
						model = Transform(tem);
						SetModelForCache(model);
						return model;
					}
				}
				else
				{
					return model;
				}
			}
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取AdvertisingPosition表记录时出现异常", e);

                return null;
            }
        }
		#endregion

		#region 从IIS缓存中获取指定条件的记录
        /// <summary>
        /// 从IIS缓存中获取指定条件的记录
        /// </summary>
        /// <param name="conditionColName">条件列名</param>
        /// <param name="value">条件值</param>
        /// <returns>DataAccess.Model.AdvertisingPosition</returns>
        public DataAccess.Model.AdvertisingPosition GetModelForCache(string conditionColName, object value)
        {
			try
            {
                //从缓存中获取List
                var list = GetList();
                DataAccess.Model.AdvertisingPosition model = null;
                Expression<Func<AdvertisingPosition, bool>> expression = null;

                //返回指定条件的实体
                switch (conditionColName)
                {
					case "Id" :
						model = list.SingleOrDefault(x => x.Id == (int)value);
                        expression = x => x.Id == (int)value;
                        break;
					case "Name" :
						model = list.SingleOrDefault(x => x.Name == (string)value);
                        expression = x => x.Name == (string)value;
                        break;
					case "ParentId" :
						model = list.SingleOrDefault(x => x.ParentId == (int)value);
                        expression = x => x.ParentId == (int)value;
                        break;
					case "Depth" :
						model = list.SingleOrDefault(x => x.Depth == (int)value);
                        expression = x => x.Depth == (int)value;
                        break;
					case "Sort" :
						model = list.SingleOrDefault(x => x.Sort == (int)value);
                        expression = x => x.Sort == (int)value;
                        break;
					case "Keyword" :
						model = list.SingleOrDefault(x => x.Keyword == (string)value);
                        expression = x => x.Keyword == (string)value;
                        break;
					case "MapImg" :
						model = list.SingleOrDefault(x => x.MapImg == (string)value);
                        expression = x => x.MapImg == (string)value;
                        break;
					case "IsDisplay" :
						model = list.SingleOrDefault(x => x.IsDisplay == (byte)value);
                        expression = x => x.IsDisplay == (byte)value;
                        break;
					case "Width" :
						model = list.SingleOrDefault(x => x.Width == (int)value);
                        expression = x => x.Width == (int)value;
                        break;
					case "Height" :
						model = list.SingleOrDefault(x => x.Height == (int)value);
                        expression = x => x.Height == (int)value;
                        break;
					case "PicImg" :
						model = list.SingleOrDefault(x => x.PicImg == (string)value);
                        expression = x => x.PicImg == (string)value;
                        break;
					case "Manager_Id" :
						model = list.SingleOrDefault(x => x.Manager_Id == (int)value);
                        expression = x => x.Manager_Id == (int)value;
                        break;
					case "Manager_CName" :
						model = list.SingleOrDefault(x => x.Manager_CName == (string)value);
                        expression = x => x.Manager_CName == (string)value;
                        break;
					case "AddDate" :
						model = list.SingleOrDefault(x => x.AddDate == (DateTime)value);
                        expression = x => x.AddDate == (DateTime)value;
                        break;

                    default :
                        return null;
                }

                if (model == null)
                {
                    //从数据库中读取
                    var tem = AdvertisingPosition.SingleOrDefault(expression);
                    if (tem == null)
                    {
                        return null;
                    }
                    else
                    {
                        //对查询出来的实体进行转换
                        model = Transform(tem);
						SetModelForCache(model);
                        return model;
                    }
                }
                else
                {
                    return model;
                }
            }
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取AdvertisingPosition表记录时出现异常", e);

                return null;
            }
        }
        #endregion

		#region 从IIS缓存中获取指定条件的记录
        /// <summary>
        /// 从IIS缓存中获取指定条件的记录
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>DataAccess.Model.AdvertisingPosition</returns>
        public DataAccess.Model.AdvertisingPosition GetModelForCache(Expression<Func<DataAccess.Model.AdvertisingPosition, bool>> expression)
        {
			//从缓存中读取记录列表
			var list = GetList();
            //如果条件为空，则查询全表所有记录
            if (expression == null)
            {
                //查找并返回记录实体
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                else
                {
                    return list.First();
                }
            }
            else
            {
                //查找并返回记录实体
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                else
                {
					//先进行条件筛选，得出的数据，再取第一个
                    var tmp = list.AsQueryable().Where(expression);
                    if (tmp.Any())
                    {
                        return tmp.First();
                    }

                    return null;
                }
            }
        }
        #endregion

		#region 更新IIS缓存中指定Id记录
		/// <summary>
        /// 更新IIS缓存中指定Id记录
        /// </summary>
        /// <param name="model">记录实体</param>
        public void SetModelForCache(DataAccess.Model.AdvertisingPosition model)
        {
			if (model == null) return;
			
            //从缓存中删除记录
            DelCache(model.Id);

            //从缓存中读取记录列表
            var list = GetList();
		    if (list == null)
		    {
                list = new List<DataAccess.Model.AdvertisingPosition>();
		    }
            //添加记录
            list.Add(model);
        }

        /// <summary>
        /// 更新IIS缓存中指定Id记录
        /// </summary>
        /// <param name="model">记录实体</param>
        public void SetModelForCache(AdvertisingPosition model)
        {
            SetModelForCache(Transform(model));
        }
		#endregion

		#region 删除IIS缓存中指定Id记录
        /// <summary>
        /// 删除IIS缓存中指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
        public bool DelCache(long id)
        {
            //从缓存中获取List
            var list = GetList(true);
            if (list == null || list.Count == 0)
            {
                return false;
            }
            else
            {
                //找到指定主键Id的实体
                var model = list.SingleOrDefault(x => x.Id == id);
                //删除指定Id的记录
                return model != null && list.Remove(model);
            }
        }

		/// <summary>
        /// 批量删除IIS缓存中指定Id记录
        /// </summary>
        /// <param name="ids">主键Id</param>
        public void DelCache(IEnumerable ids)
        {
            //循环删除指定Id队列
		    foreach (var id in ids)
		    {
		        DelCache((int)id);
		    }
        }

		/// <summary>
        /// 按条件删除IIS缓存中AdvertisingPosition表的指定记录
        /// </summary>
        /// <param name="expression">条件，值为null时删除全有记录</param>
		public void DelCache(Expression<Func<DataAccess.Model.AdvertisingPosition, bool>> expression)
        {
            //从缓存中获取List
		    var list = GetList();
            //如果缓存为null，则不做任何处理
            if (list == null || list.Count == 0)
            {
                return;
            }

            //如果条件为空，则删除全部记录
            if (expression == null)
            {
                //删除所有记录
                DelAllCache();
            }
            else
            {
                var tem = list.AsQueryable().Where(expression);
                foreach (var model in tem)
                {
                    list.Remove(model);
                }
            }
        }
		#endregion

		#region 实体转换
		/// <summary>
		/// 将AdvertisingPosition记录实体（SubSonic实体）转换为普通的实体（DataAccess.Model.AdvertisingPosition）
		/// </summary>
        /// <param name="model">SubSonic插件生成的实体</param>
		/// <returns>DataAccess.Model.AdvertisingPosition</returns>
		public DataAccess.Model.AdvertisingPosition Transform(AdvertisingPosition model)
        {			
			if (model == null) 
				return null;

            return new DataAccess.Model.AdvertisingPosition
            {
                Id = model.Id,
                Name = model.Name,
                ParentId = model.ParentId,
                Depth = model.Depth,
                Sort = model.Sort,
                Keyword = model.Keyword,
                MapImg = model.MapImg,
                IsDisplay = model.IsDisplay,
                Width = model.Width,
                Height = model.Height,
                PicImg = model.PicImg,
                Manager_Id = model.Manager_Id,
                Manager_CName = model.Manager_CName,
                AddDate = model.AddDate,
            };
        }

		/// <summary>
		/// 将AdvertisingPosition记录实体集（SubSonic实体）转换为普通的实体集（DataAccess.Model.AdvertisingPosition）
		/// </summary>
        /// <param name="sourceList">SubSonic插件生成的实体集</param>
        public IList<DataAccess.Model.AdvertisingPosition> Transform(IList<AdvertisingPosition> sourceList)
        {
			//创建List容器
            var list = new List<DataAccess.Model.AdvertisingPosition>();
			//将SubSonic插件生成的实体集转换后存储到刚创建的List容器中
            sourceList.ToList().ForEach(r => list.Add(Transform(r)));
            return list;
        }

		/// <summary>
		/// 将AdvertisingPosition记录实体由普通的实体（DataAccess.Model.AdvertisingPosition）转换为SubSonic插件生成的实体
		/// </summary>
        /// <param name="model">普通的实体（DataAccess.Model.AdvertisingPosition）</param>
		/// <returns>AdvertisingPosition</returns>
		public AdvertisingPosition Transform(DataAccess.Model.AdvertisingPosition model)
        {
			if (model == null) 
				return null;

            return new AdvertisingPosition
            {
                Id = model.Id,
                Name = model.Name,
                ParentId = model.ParentId,
                Depth = model.Depth,
                Sort = model.Sort,
                Keyword = model.Keyword,
                MapImg = model.MapImg,
                IsDisplay = model.IsDisplay,
                Width = model.Width,
                Height = model.Height,
                PicImg = model.PicImg,
                Manager_Id = model.Manager_Id,
                Manager_CName = model.Manager_CName,
                AddDate = model.AddDate,
            };
        }

		/// <summary>
		/// 将AdvertisingPosition记录实体由普通实体集（DataAccess.Model.AdvertisingPosition）转换为SubSonic插件生成的实体集
		/// </summary>
        /// <param name="sourceList">普通实体集（DataAccess.Model.AdvertisingPosition）</param>
        public IList<AdvertisingPosition> Transform(IList<DataAccess.Model.AdvertisingPosition> sourceList)
        {
			//创建List容器
            var list = new List<AdvertisingPosition>();
			//将普通实体集转换后存储到刚创建的List容器中
            sourceList.ToList().ForEach(r => list.Add(Transform(r)));
            return list;
        }
		#endregion

		#region 给实体赋值
		/// <summary>
        /// 给实体赋值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="dic">列名与值</param>
		public void SetModelValue(DataAccess.Model.AdvertisingPosition model, Dictionary<string, object> dic)
		{
			if (model == null || dic == null) return;

            //遍历字典，逐个给字段赋值
            foreach (var d in dic)
            {
                SetModelValue(model, d.Key, d.Value);
            }
		}

        /// <summary>
        /// 给实体赋值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="colName">列名</param>
        /// <param name="value">值</param>
		public void SetModelValue(DataAccess.Model.AdvertisingPosition model, string colName, object value)
		{
			if (model == null || string.IsNullOrEmpty(colName)) return;

			//返回指定条件的实体
            switch (colName)
            {
				case "Id" :
					model.Id = (int)value;
                    break;
				case "Name" :
					model.Name = (string)value;
                    break;
				case "ParentId" :
					model.ParentId = (int)value;
                    break;
				case "Depth" :
					model.Depth = (int)value;
                    break;
				case "Sort" :
					model.Sort = (int)value;
                    break;
				case "Keyword" :
					model.Keyword = (string)value;
                    break;
				case "MapImg" :
					model.MapImg = (string)value;
                    break;
				case "IsDisplay" :
					model.IsDisplay = ConvertHelper.Ctinyint(value);
                    break;
				case "Width" :
					model.Width = (int)value;
                    break;
				case "Height" :
					model.Height = (int)value;
                    break;
				case "PicImg" :
					model.PicImg = (string)value;
                    break;
				case "Manager_Id" :
					model.Manager_Id = (int)value;
                    break;
				case "Manager_CName" :
					model.Manager_CName = (string)value;
                    break;
				case "AddDate" :
					model.AddDate = (DateTime)value;
                    break;
            }
		}

        #endregion

		#endregion

		#region 获取AdvertisingPosition表记录总数
        /// <summary>
        /// 获取AdvertisingPosition表记录总数
        /// </summary>
        /// <returns>记录总数</returns>
        public int GetRecordCount()
        {
            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
				//从缓存中获取记录集
                var list = GetList();
                return list == null ? 0 : list.Count;
            }
			else
			{
				//从数据库中查询记录集数量
				var select = new SelectHelper();
				return select.GetRecordCount<AdvertisingPosition>();
			}
        }

		/// <summary>
		/// 获取AdvertisingPosition表记录总数——从数据库中查询
		/// </summary>
        /// <param name="wheres">条件</param>
		/// <returns>int</returns>
		public int GetRecordCount(List<ConditionHelper.SqlqueryCondition> wheres) {
			var select = new SelectHelper();
			return select.GetRecordCount<AdvertisingPosition>(wheres);

		}

		/// <summary>
		/// 获取AdvertisingPosition表指定条件的记录总数——从数据库中查询
		/// </summary>
        /// <param name="expression">条件</param>
		/// <returns>int</returns>
		public int GetRecordCount(Expression<Func<AdvertisingPosition, bool>> expression) {
            return new Select().From<AdvertisingPosition>().Where(expression).GetRecordCount();
		}

        #endregion

		#region 查找指定条件的记录集合
        /// <summary>
        /// 查找指定条件的记录集合——从IIS缓存中查找
        /// </summary>
        /// <param name="expression">条件语句</param>
        public IList<DataAccess.Model.AdvertisingPosition> Find(Expression<Func<DataAccess.Model.AdvertisingPosition, bool>> expression)
        {
			//从缓存中获取记录集
			var list = GetList();
            //判断获取记录集是否为null
            if (list == null)
            {
                return null;
            }
            else
            {
                //在返回的记录集中查询
                var result = list.AsQueryable().Where(expression);
                //不存在指定记录集
                if (!result.Any())
                    return null;
                else
                    return result.ToList();
            }
        }
		#endregion

		#region 判断指定条件的记录是否存在
        /// <summary>
        /// 判断指定主键Id的记录是否存在——在IIS缓存或数据库中查找
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public bool Exist(int id)
        {
            if (id <= 0)
                return false;

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                return Exist(x => x.Id == id);
            }
            
            //从数据库中查找
            return AdvertisingPosition.Exists(x => x.Id == id);
        }

        /// <summary>
        /// 判断指定条件的记录是否存在——默认在IIS缓存中查找，如果没开启缓存时，则直接在数据库中查询出列表后，再从列表中查询
        /// </summary>
        /// <param name="expression">条件语句</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<DataAccess.Model.AdvertisingPosition, bool>> expression)
        {
            var list = GetList();
            if (list == null) 
                return false;
            else
            {
                return list.AsQueryable().Any(expression);
            }
        }
        #endregion

		#region 获取AdvertisingPosition表记录
		/// <summary>
		/// 获取AdvertisingPosition表记录
		/// </summary>
		/// <param name="norepeat">是否使用去重复</param>
		/// <param name="top">获取指定数量记录</param>
		/// <param name="columns">获取指定的列记录</param>
		/// <param name="pageIndex">当前分页页面索引</param>
		/// <param name="pageSize">每个页面记录数量</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序方式</param>
        /// <returns>返回DataTable</returns>
		public DataTable GetDataTable(bool norepeat = false, int top = 0, List<string> columns = null, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null) {
			try
            {
                //分页查询
                var select = new SelectHelper();
                return select.SelectDataTable<AdvertisingPosition>(norepeat, top, columns, pageIndex, pageSize, wheres, sorts);
            }
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("获取AdvertisingPosition表记录时出现异常", e);

                return null;
            }
		}
		#endregion

		#region 绑定Grid表格
		/// <summary>
		/// 绑定Grid表格，并实现分页
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="pageIndex">第几页</param>
		/// <param name="pageSize">每页显示记录数量</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序</param>
		public override void BindGrid(FineUI.Grid grid, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null) {
			//用于统计执行时长(耗时)
			var swatch = new Stopwatch();
			swatch.Start();

			try {
				// 1.设置总项数
				grid.RecordCount = GetRecordCount(wheres);
				// 2.如果不存在记录，则清空Grid表格
				if (grid.RecordCount == 0) {
					grid.Rows.Clear();
					// 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = false;
				}
				else
				{
					//3.查询并绑定到Grid
					grid.DataSource = GetDataTable(false, 0, null, pageIndex, pageSize, wheres, sorts);
					grid.DataBind();
				}
			}
			catch (Exception e) {
				// 记录日志
				CommonBll.WriteLog("获取用户操作日志表记录时出现异常", e);

			}

			// 统计结束
			swatch.Stop();
			// 计算查询数据库使用时间，并存储到Session里，以便UI显示
			HttpContext.Current.Session["SpendingTime"] = (swatch.ElapsedMilliseconds / 1000.00).ToString();
		}
		#endregion

		#region 绑定Grid表格
		/// <summary>
		/// 绑定Grid表格，使用内存分页，显示有层次感
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="parentValue">父Id值</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序</param>
		/// <param name="parentId">父Id</param>
		public override void BindGrid(FineUI.Grid grid, int parentValue, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null, string parentId = "ParentId") {
			//用于统计执行时长(耗时)
			var swatch = new Stopwatch();
			swatch.Start();

			try
			{
				// 查询数据库
				var dt = GetDataTable(false, 0, null, 0, 0, wheres, sorts);
                
                // 1.设置总项数
                grid.RecordCount = dt == null ? 0 : dt.Rows.Count;
                // 2.如果不存在记录，则清空Grid表格
                if (grid.RecordCount == 0)
                {
                    grid.Rows.Clear();
                    // 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = false;
                }
                else
                {
                    // 对查询出来的记录进行层次处理
                    grid.DataSource = DataTableHelper.DataTableTidyUp(dt, "Id", parentId, parentValue);
                    // 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = true;
                }
			}
			catch (Exception e) {
				// 记录日志
				CommonBll.WriteLog("绑定表格时出现异常", e);

			}

			//统计结束
			swatch.Stop();
			//计算查询数据库使用时间，并存储到Session里，以便UI显示
			HttpContext.Current.Session["SpendingTime"] = (swatch.ElapsedMilliseconds / 1000.00).ToString();
		}

		/// <summary>
		/// 绑定Grid表格，使用内存分页，显示有层次感
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="parentValue">父Id值</param>
		/// <param name="sorts">排序</param>
		/// <param name="parentId">父Id</param>
		public override void BindGrid(FineUI.Grid grid, int parentValue, List<string> sorts = null, string parentId = "ParentId") {
			BindGrid(grid, parentValue, null, sorts, parentId);
		}
		#endregion

		#region 添加与编辑AdvertisingPosition表记录
		/// <summary>
		/// 添加与编辑AdvertisingPosition记录
		/// </summary>
	    /// <param name="page">当前页面指针</param>
		/// <param name="model">AdvertisingPosition表实体</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void Save(Page page, AdvertisingPosition model, string content = null, bool isCache = true, bool isAddUseLog = true)
        {
			try {
				//保存
				model.Save();
				
				//判断是否启用缓存
			    if (CommonBll.IsUseCache() && isCache)
			    {
                    SetModelForCache(model);
			    }
				
				if (isAddUseLog)
				{
					if (string.IsNullOrEmpty(content))
					{
						content = "{0}" + (model.Id == 0 ? "添加" : "编辑") + "AdvertisingPosition记录成功，ID为【" + model.Id + "】";
					}

					//添加用户访问记录
					UseLogBll.GetInstence().Save(page, content);
				}
			}
			catch (Exception e) {
				var result = "执行AdvertisingPositionBll.Save()函数出错！";

				//出现异常，保存出错日志信息
				CommonBll.WriteLog(result, e);
			}
		}
		#endregion

		#region 删除AdvertisingPosition表记录
		/// <summary>
		/// 删除AdvertisingPosition表记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public override void Delete(Page page, int id, bool isAddUseLog = true) 
		{
			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} = {2}", AdvertisingPositionTable.TableName,  AdvertisingPositionTable.Id, id);

			//删除
			var delete = new DeleteHelper();
		    delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(id);
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了AdvertisingPosition表id为【" + id + "】的记录！");
			}
		}

		/// <summary>
		/// 删除AdvertisingPosition表记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public override void Delete(Page page, int[] id, bool isAddUseLog = true) 
		{
			if (id == null) return;
			//将数组转为逗号分隔的字串
			var str = string.Join(",", id);

			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} in ({2})", AdvertisingPositionTable.TableName,  AdvertisingPositionTable.Id, str);

			//删除
			var delete = new DeleteHelper();
		    delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(id.ToList());
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了AdvertisingPosition表id为【" + str + "】的记录！");
			}
		}

		/// <summary>
        /// 删除AdvertisingPosition表记录——如果使用了缓存，删除成功后会清空本表的所有缓存记录，然后重新加载进缓存
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="expression">条件语句</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void Delete(Page page, Expression<Func<AdvertisingPosition, bool>> expression, bool isAddUseLog = true)
        {
			//执行删除
			AdvertisingPosition.Delete(expression);

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
				//清空当前表所有缓存记录
				DelAllCache();
                //重新载入缓存
                GetList();
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了AdvertisingPosition表记录！");
			}
        }

		/// <summary>
        /// 删除AdvertisingPosition表所有记录
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void DeleteAll(Page page, bool isAddUseLog = true)
        {
            //设置Sql语句
            var sql = string.Format("delete from {0}", AdvertisingPositionTable.TableName);

            //删除
            var delete = new DeleteHelper();
            delete.Delete(sql);

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //清空当前表所有缓存记录
                DelAllCache();
            }

            if (isAddUseLog)
            {
                //添加用户操作记录
                UseLogBll.GetInstence().Save(page, "{0}删除了AdvertisingPosition表所有记录！");
            }
        }
		#endregion
		
		#region 保存列表排序
		/// <summary>
		/// 保存列表排序，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="grid1">页面表格</param>
		/// <param name="tbxSort">表格中绑定排序的表单名</param>
		/// <param name="sortName">排序字段名</param>
		/// <returns>更新成功返回true，失败返回false</returns>
		public override bool UpdateSort(Page page, FineUI.Grid grid1, string tbxSort, string sortName = "Sort")
	    {
		     //更新排序
			if (CommonBll.UpdateSort(page, grid1, tbxSort, "AdvertisingPosition", sortName, "Id"))
		    {
				//判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //删除所有缓存
                    DelAllCache();
                    //重新载入缓存
                    GetList();
                }
				
			    //添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}更新了AdvertisingPosition表排序！");

			    return true;
		    }

			return false;
	    }
		#endregion

		#region 自动排序
		/// <summary>自动排序，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="strWhere">附加Where : " sid=1 "</param>
		/// <param name="isExistsMoreLv">是否存在多级分类,一级时,请使用false,多级使用true，(一级不包括ParentID字段)</param>
		/// <param name="pid">父级分类的ParentID</param>
		/// <param name="fieldName">字段名:"SortId"</param>
		/// <param name="fieldParentId">字段名:"ParentId"</param>
		/// <returns>更新成功返回true，失败返回false</returns>
		public override bool UpdateAutoSort(Page page, string strWhere = "", bool isExistsMoreLv = false, int pid = 0, string fieldName = "Sort", string fieldParentId = "ParentId")
	    {
		    //更新排序
			if (CommonBll.AutoSort("Id", "AdvertisingPosition", strWhere, isExistsMoreLv, pid, fieldName, fieldParentId))
		    {
				//判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //删除所有缓存
                    DelAllCache();
                    //重新载入缓存
                    GetList();
                }

			    //添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}对AdvertisingPosition表进行了自动排序操作！");

			    return true;
		    }

			return false;
	    }
		#endregion
		
		#region 获取数据表中的某个值
		/// <summary>
        /// 获取数据表中的某个值——主要用于内存查询，数据量大的表请将isCache设为false
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="colName">获取的列名</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns>指定列的值</returns>
        public object GetFieldValue(int id, string colName, bool isCache = true)
	    {
			return GetFieldValue(colName, null, id, isCache);            
	    }

	    /// <summary>
        /// 获取数据表中的某个值——主要用于内存查询，数据量大的表请将isCache设为false
	    /// </summary>
	    /// <param name="colName">获取的列名</param>
	    /// <param name="conditionColName">条件列名，为null时默认为主键Id</param>
	    /// <param name="value">条件值</param>
	    /// <param name="isCache">是否从缓存中读取</param>
	    /// <returns></returns>
	    public object GetFieldValue(string colName, string conditionColName, object value, bool isCache = true)
	    {
            //在内存中查询
	        if (isCache)
	        {
                //判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //如果条件列为空，则默认为主键列
                    if (string.IsNullOrEmpty(conditionColName))
                    {
                        //获取实体
                        var model = GetModelForCache(ConvertHelper.Cint0(value));
                        //返回指定字段名的值
                        return GetFieldValue(model, colName);
                    }
                    else
                    {
                        //获取实体
                        var model = GetModelForCache(conditionColName, value);
                        //返回指定字段名的值
                        return GetFieldValue(model, colName);
                    }
                }

				//递归调用，从数据库中查询
	            return GetFieldValue(colName, conditionColName, value, false);
	        }
            //从数据库中查询
	        else
	        {
				if (string.IsNullOrEmpty(conditionColName))
	            {
	                conditionColName = AdvertisingPositionTable.Id;
	            }

                //设置条件
                var wheres = new List<ConditionHelper.SqlqueryCondition>();
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, conditionColName, Comparison.Equals, value));

                return GetFieldValue(colName, wheres);
	        }
	    }
		
	    /// <summary>
        /// 获取数据表中的某个值——使用IIS缓存查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, Expression<Func<DataAccess.Model.AdvertisingPosition, bool>> expression)
	    {
	        return GetFieldValue(GetModelForCache(expression), colName);
	    }

	    /// <summary>
        /// 获取数据表中的某个值——从数据库中查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="wheres">条件，例：Id=100 and xx=20</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, string wheres)
        {
            try
            {
                return DataTableHelper.DataTable_Find_Value(GetDataTable(), wheres, colName);
			}
			catch (Exception e)
			{
                //记录日志
                CommonBll.WriteLog("查询数据出现异常", e);
			}

            return null;
        }

        /// <summary>
        /// 获取数据表中的某个值——从数据库中查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="wheres">条件</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, List<ConditionHelper.SqlqueryCondition> wheres)
        {
            var select = new SelectHelper();
            return select.GetColumnsValue<AdvertisingPosition>(colName, wheres);
        }

		/// <summary>
        /// 返回实体中指定字段名的值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="colName">获取的字段名</param>
        /// <returns></returns>
		private object GetFieldValue(DataAccess.Model.AdvertisingPosition model, string colName)
		{
			if (model == null || string.IsNullOrEmpty(colName)) return null;
			//返回指定的列值
			switch (colName)
			{
				case "Id" :
					return model.Id;
				case "Name" :
					return model.Name;
				case "ParentId" :
					return model.ParentId;
				case "Depth" :
					return model.Depth;
				case "Sort" :
					return model.Sort;
				case "Keyword" :
					return model.Keyword;
				case "MapImg" :
					return model.MapImg;
				case "IsDisplay" :
					return model.IsDisplay;
				case "Width" :
					return model.Width;
				case "Height" :
					return model.Height;
				case "PicImg" :
					return model.PicImg;
				case "Manager_Id" :
					return model.Manager_Id;
				case "Manager_CName" :
					return model.Manager_CName;
				case "AddDate" :
					return model.AddDate;
			}

			return null;
		}

		#endregion
		
		#region 更新AdvertisingPosition表指定字段值
		/// <summary>更新AdvertisingPosition表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="dic">需要更新的字段与值</param>
		/// <param name="wheres">条件</param>
		/// <param name="content">更新说明</param>
		/// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateValue(Page page, Dictionary<string, object> dic, List<ConditionHelper.SqlqueryCondition> wheres = null, string content = "", bool isCache = true, bool isAddUseLog = true) {
			//更新
			var update = new UpdateHelper();
			update.Update<AdvertisingPosition>(dic, wheres);

			//判断是否启用缓存
			if (isCache && CommonBll.IsUseCache())
			{
				//删除全部缓存	
				DelAllCache();
				//重新载入缓存
				GetList();
			}
			
			if (isAddUseLog){
				if (string.IsNullOrEmpty(content))
				{
					//添加用户操作记录
					UseLogBll.GetInstence().Save(page, content != "" ? content : "{0}修改了AdvertisingPosition表记录。");				
				}
				else
				{
					//添加用户操作记录
					UseLogBll.GetInstence().Save(page, content);
				}
			}
		}
		#endregion
				
		#region 更新AdvertisingPosition表指定主键Id的字段值
		/// <summary>更新AdvertisingPosition表记录指定字段值</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
	    /// <param name="dic">需要更新的字段与值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue(Page page, int id, Dictionary<string, object> dic, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了AdvertisingPosition表主键Id值为" + id + "的记录。";
			
            //条件
		    List<ConditionHelper.SqlqueryCondition> wheres = null;
            if (id > 0)
            {
                wheres = new List<ConditionHelper.SqlqueryCondition>();
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisingPositionTable.Id, Comparison.Equals, id));
            };

			//判断是否启用缓存——为了防止并发问题，所以先更新缓存再更新数据库
			if (isCache && CommonBll.IsUseCache())
			{
				//从缓存中获取实体
				var model = GetModelForCache(id);
				if (model != null)
				{
					//给获取的实体赋值
					SetModelValue(model, dic);
					//更新缓存中的实体
					SetModelForCache(model);
				}
			}

            //执行更新
            UpdateValue(page, dic, wheres, content, false, isAddUseLog);
        }

        /// <summary>更新AdvertisingPosition表记录指定字段值（更新一个字段值）</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
        /// <param name="columnName">要更新的列名</param>
        /// <param name="columnValue">要更新的列值</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void UpdateValue(Page page, int id, string columnName, object columnValue, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
            content = content != "" ? content : "{0}修改了AdvertisingPosition表主键Id值为" + id + "的记录，将" + columnName + "字段值修改为" + columnValue;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName, columnValue);

			//执行更新
            UpdateValue(page, id, dic, content, isCache, isAddUseLog);
        }

		 /// <summary>更新AdvertisingPosition表记录指定字段值（更新两个字段值）</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
        /// <param name="columnName1">要更新的列名</param>
        /// <param name="columnValue1">要更新的列值</param>
        /// <param name="columnName2">要更新的列名</param>
        /// <param name="columnValue2">要更新的列值</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void UpdateValue(Page page, int id, string columnName1, object columnValue1, string columnName2, object columnValue2, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
            content = content != "" ? content : "{0}修改了AdvertisingPosition表主键Id值为" + id + "的记录，将" + columnName1 + "字段值修改为" + columnValue1 + "，" + columnName2 + "字段值修改为" + columnValue2;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName1, columnValue1);
            dic.Add(columnName2, columnValue2);

			//执行更新
            UpdateValue(page, id, dic, content, isCache, isAddUseLog);
        }
        #endregion
		
		#region 获取Name字段值
        /// <summary>
        /// 获取Name字段值
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns></returns>
        public string GetName(Page page, int pkValue, bool isCache = true)
        {
            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                var model = GetModelForCache(pkValue);
                return model == null ? "" : model.Name;
            }
            else
            {
                //从数据库中查询
                var model = AdvertisingPosition.SingleOrDefault(x => x.Id == pkValue);
                return model == null ? "" : model.Name;
            }
        }
        #endregion

		#region 使用Keyword来查询，获取一个AdvertisingPosition实体对象
        /// <summary>使用Key来查询，获取一个AdvertisingPosition实体对象</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="key">Key值</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns>DataAccess.Model.AdvertisingPosition 实体</returns>
        public DataAccess.Model.AdvertisingPosition GetModel_ByKeyword(Page page, string key, bool isCache = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                return GetModelForCache(x => x.Keyword == key);
            }
			else
			{
				//从数据库中查询
				return Transform(AdvertisingPosition.SingleOrDefault(x => x.Keyword == key));
			}
        }
        #endregion

		#region 删除MapImg字段存储的对应图片
		/// <summary>删除MapImg字段存储的对应图片</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否同步更新缓存</param>
		public void DelMapImg(Page page, int pkValue, bool isCache = true) 
		{
			try {
				string img = "";

				//设置条件
				var wheres = new List<ConditionHelper.SqlqueryCondition>();
				wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisingPositionTable.Id, Comparison.Equals, pkValue));

				//获取图片地址
				var select = new SelectHelper();
				img = select.GetColumnsValue<AdvertisingPosition>(AdvertisingPositionTable.MapImg, wheres) + "";

				//删除图片
				UploadFileBll.GetInstence().Upload_OneDelPic(img);

				//设置更新值
				var setValue = new Dictionary<string, object>();
				setValue[AdvertisingPositionTable.MapImg] = "";
				//更新
				UpdateValue(page, setValue, wheres, "{0}更新了Ad表id为【" + pkValue + "】的记录，将图片Img删除", false);

                //判断是否启用缓存
                if (isCache && CommonBll.IsUseCache())
                {
                    //从缓存中获取实体
                    var model = GetModelForCache(pkValue);
					if (model != null)
					{
						//给获取的实体赋值
						SetModelValue(model, AdvertisingPositionTable.MapImg, "");
						//更新缓存中的实体
						SetModelForCache(model);
					}
                }
			}
			catch (Exception e) {
				//出现异常，保存出错日志信息
				CommonBll.WriteLog("", e);
			}
		}
		#endregion

		#region 更新IsDisplay字段值
		/// <summary>
		/// 更新IsDisplay字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsDisplay(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[AdvertisingPositionTable.IsDisplay] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了AdvertisingPosition表id为【" + pkValue + "】的记录，更新内容为将IsDisplay字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 删除PicImg字段存储的对应图片
		/// <summary>删除PicImg字段存储的对应图片</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否同步更新缓存</param>
		public void DelPicImg(Page page, int pkValue, bool isCache = true) 
		{
			try {
				string img = "";

				//设置条件
				var wheres = new List<ConditionHelper.SqlqueryCondition>();
				wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisingPositionTable.Id, Comparison.Equals, pkValue));

				//获取图片地址
				var select = new SelectHelper();
				img = select.GetColumnsValue<AdvertisingPosition>(AdvertisingPositionTable.PicImg, wheres) + "";

				//删除图片
				UploadFileBll.GetInstence().Upload_OneDelPic(img);

				//设置更新值
				var setValue = new Dictionary<string, object>();
				setValue[AdvertisingPositionTable.PicImg] = "";
				//更新
				UpdateValue(page, setValue, wheres, "{0}更新了Ad表id为【" + pkValue + "】的记录，将图片Img删除", false);

                //判断是否启用缓存
                if (isCache && CommonBll.IsUseCache())
                {
                    //从缓存中获取实体
                    var model = GetModelForCache(pkValue);
					if (model != null)
					{
						//给获取的实体赋值
						SetModelValue(model, AdvertisingPositionTable.PicImg, "");
						//更新缓存中的实体
						SetModelForCache(model);
					}
                }
			}
			catch (Exception e) {
				//出现异常，保存出错日志信息
				CommonBll.WriteLog("", e);
			}
		}
		#endregion

		#region 获取排序字段Sort的最大值
		/// <summary>
		/// 获取排序字段Sort的最大值
		/// </summary>
		public int GetSortMax() 
		{
			//查询
			var select = new SelectHelper();
		    return ConvertHelper.Cint0(select.GetMax<AdvertisingPosition>(AdvertisingPositionTable.Sort));
		}
		#endregion
		
		#region 获取排序字段Sort的最大值
		/// <summary>
		/// 获取排序字段Sort的最大值
		/// </summary>
		/// <param name="parentId">父Id值</param>
		public int GetSortMax(int parentId) 
		{
			//定义查询条件
			var wheres = new List<ConditionHelper.SqlqueryCondition>();
			wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisingPositionTable.ParentId, Comparison.Equals, parentId));
			//查询
			var select = new SelectHelper();
		    return ConvertHelper.Cint0(select.GetMax<AdvertisingPosition>(AdvertisingPositionTable.Sort, wheres));
		}
		#endregion
		
    
		#endregion 模版生成函数

    } 
}
