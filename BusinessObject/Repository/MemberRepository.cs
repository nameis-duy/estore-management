using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetMemberList() => MemberDAO.Instance.GetMemberList();
        public Member? GetMemberById(int id) => MemberDAO.Instance.GetMemberById(id);
        public void AddMember(Member member) => MemberDAO.Instance.AddMember(member);
        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
        public void RemoveMember(int id) => MemberDAO.Instance.RemoveMember(id);
    }
}
