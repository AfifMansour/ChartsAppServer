using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class GraphService 
    {
        public GraphService()
        {
        }

        public async Task<IApiResponse<HealthStatementResponse>> GetFirstGraph()
        {

            IApiResponse<HealthStatementResponse> response = new ApiResponse<HealthStatementResponse>();
            var HealthStatementResponse = new HealthStatementResponse();

            var HealthStatement = await _healthStatementRepository.GetHealthStatement(userId, childUserId);

            if (HealthStatement != null)
            {

                // check if updateDate is in last 12 hours, if so --> update isDeclared to true. 
                if (!isTeacher && childUserId.HasValue)
                {
                    HealthStatementResponse.ForUserId = childUserId.Value;
                    HealthStatementResponse.ForUserName = HealthStatement.ForUser.FirstName + " " + HealthStatement.ForUser.LastName;
                    HealthStatementResponse.ForUserExtId = HealthStatement.ForUser.ExternalId;
                }

                HealthStatementResponse.Id = HealthStatement.Id;
                HealthStatementResponse.UserId = HealthStatement.UserId;
                HealthStatementResponse.UserName = HealthStatement.User.FirstName + " " + HealthStatement.User.LastName;
                HealthStatementResponse.UserExtId = HealthStatement.User.ExternalId;
                HealthStatementResponse.UpdateDate = HealthStatement.UpdateDate;

                HealthStatementResponse.InstituteId = HealthStatement.User.Institute.Id;
                HealthStatementResponse.InstituteName = HealthStatement.User.Institute.Name;

                if (DateTime.Now.Date == HealthStatementResponse.UpdateDate.Date)
                {
                    HealthStatementResponse.IsDeclared = true;
                }

                response.Data = HealthStatementResponse;
            }
            else
            {
                if (!isTeacher && childUserId.HasValue)
                {
                    var child = await _userRepository.Get(childUserId.Value);
                    HealthStatementResponse.ForUserId = childUserId.Value;
                    HealthStatementResponse.ForUserName = child.FirstName + " " + child.LastName;
                    HealthStatementResponse.ForUserExtId = child.ExternalId;
                }

                var user = await _userRepository.Get(userId);
                HealthStatementResponse.UserId = userId;
                HealthStatementResponse.UserExtId = user.ExternalId;
                HealthStatementResponse.UserName = user.FirstName + " " + user.LastName;
                HealthStatementResponse.InstituteId = user.Institute.Id;
                HealthStatementResponse.InstituteName = user.Institute.Name;
            }

            response.Data = HealthStatementResponse;

            return response;
        }

        public async Task<IApiResponse<int>> SetHealthStatement(HealthStatementRequest healthStatementRequest)
        {
            IApiResponse<int> response = new ApiResponse<int>();
            HealthStatement healthStatement = new HealthStatement()
            {
                UserId = healthStatementRequest.UserId,
                UpdateDate = DateTime.Now,
                ForUserId = healthStatementRequest.ForUserId,
                IsDeleted = false
            };
            response.Data = await _healthStatementRepository.SetHealthStatement(healthStatement);
            return response;
        }
    }
}
