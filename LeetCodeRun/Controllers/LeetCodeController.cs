using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeetCodeRun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeetCodeController : ControllerBase
    {
        [HttpGet]
        [Route("Execution")]
        public async Task<IActionResult> Execution()
        {
            /*====================Contains Duplicate==============*/
            //int[] nums = { 0 };
            //var response = ContainsDuplicate(nums);

            /*==============Two Sum=================*/
            int[] nums = { 3, 3 };
            var response = TwoSum(nums, 6);



            return Ok(response);
        }

        [HttpPost]
        [Route("ContainsDuplicate")]
        public bool ContainsDuplicate(int[] nums)
        {

            int len = nums.Length;
            //int?[] numList = new int?[len];
            HashSet<int?> numList = new HashSet<int?>();

            for (int i = 0; i < len; i++)
            {
                if (numList.Contains(nums[i]))
                {
                    return true;
                }
                else
                {
                    numList.Add(nums[i]);
                }
            }

            return false;
        }

        [HttpPost]
        [Route("TwoSum")]
        public int[] TwoSum(int[] nums, int target)
        {
            int len = nums.Length; int lookingFor = 0;
            int[] resultList = new int[2];

            for (int i = 0; i < len; i++)
            {
                lookingFor = target - nums[i];

                int index = Array.IndexOf(nums, lookingFor);
                if (index < 0)
                {
                    continue;
                }
                else
                {
                    if (i == index)
                    {
                        //index = i - 1 > 0 ? Array.IndexOf(nums, lookingFor, 0, i-1) :
                        //    (index < 0 || i == index) ? Array.IndexOf(nums, lookingFor,i < len ? i + 1 : i, i==len ? len - 1 : len) : -1;

                        if (i - 1 > 0)
                        {
                            index = Array.IndexOf(nums, lookingFor, 0, i - 1);
                        }
                        else if (index < 0 || i == index)
                        {
                            index = Array.IndexOf(nums, lookingFor, i < len ? i + 1 : i, len - 1);
                        }
                        else
                        {
                            index = -1;
                        }

                        //index = index < 0 || i == index ? Array.IndexOf(nums, lookingFor, i + 1, len-1) : 0;
                    }

                    if (index < 0)
                    {
                        continue;
                    }

                    if (i < index)
                    {
                        resultList[0] = i;
                        resultList[1] = index;
                        break;
                    }
                    else
                    {
                        resultList[0] = index;
                        resultList[1] = i;
                        break;
                    }
                }

            }
            return resultList;
        }

        [HttpPost]
        [Route("SimplifyPath")]
        public string SimplifyPath(string path)
        {
            char[] result = new char[path.Length];
            int resultIndex = 1;

            result[0] = path[0];

            for (int i = 1; i < path.Length; i++)
            {
                if (result[resultIndex - 1] == '/' && (path[i] == '/' || path[i] == '.'))
                {
                    continue;
                }
                else
                {
                    result[resultIndex] = path[i];
                    resultIndex++;
                }
            }
            if (resultIndex > 1 && result[resultIndex - 1] == '/')
            {
                result[resultIndex - 1] = '\0';
            }
            char[] filteredCharArray = result.Where(c => c != '\0').ToArray();
            string strResult = new string(filteredCharArray);
            return strResult;
        }
    }
}
