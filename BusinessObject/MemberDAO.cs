using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO? instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------
        public IEnumerable<Member> GetMemberList()
        {
            var members = new List<Member>();
            try
            {
                using var context = new MyFStoreContext();
                members = context.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }
        public Member?  GetMemberById(int id)
        {
            Member? member = null;
            try
            {
                using var context = new MyFStoreContext();
                member = context.Members.SingleOrDefault(mem => mem.MemberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void AddMember(Member member)
        {
            try
            {
                var _member = GetMemberById(member.MemberId);
                if (_member == null)
                {
                    using var context = new MyFStoreContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateMember(Member member)
        {
            try
            {
                var _member = GetMemberById(member.MemberId);
                if (_member != null)
                {
                    using var context = new MyFStoreContext();
                    context.Members.Update(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveMember(int id)
        {
            try
            {
                var member = GetMemberById(id);
                if (member != null)
                {
                    using var context = new MyFStoreContext();
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
