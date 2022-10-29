using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository 
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMemberList();
        Member? GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void RemoveMember(int id);
    }
}
