using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.ComponentModel;

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

            for (int i = 0; i < len - 1; i++)
            {
                lookingFor = target - nums[i];

                int index = Array.IndexOf(nums, lookingFor, i + 1);

                if (index < 0)
                {
                    continue;
                }
                else
                {
                    resultList[0] = i;
                    resultList[1] = index;
                    break;
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
        
        [HttpPost]
        [Route("SimplifyPathSolution_02")]
        public string SimplifyPath2(string path)
        {
            string[] components = path.Split('/');
            Stack<string> stack = new Stack<string>();

            foreach (string component in components)
            {
                if (component == "" || component == ".")
                {
                    // ignore empty components and current directory components
                    continue;
                }
                else if (component == "..")
                {
                    // remove the last component from the stack if it exists
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    // add the component to the stack
                    stack.Push(component);
                }
            }

            // join the stack components with slashes to form the output path
            string[] outputComponents = stack.Reverse().ToArray();
            string outputPath = "/" + string.Join("/", outputComponents);

            return outputPath;
        }
        public class ValidateStackSequenceVM
        {
            public int[] pushed { get; set; }
            public int[] popped { get; set; }
        }
        [HttpPost]
        [Route("ValidateStackSequences")]
        [SwaggerOperation(Tags = new[] { "Input:  >>>>> Output:  >>>>>>> Input:  >>>>>>> Output: " })]
        public bool ValidateStackSequences(ValidateStackSequenceVM model)
        {
            bool result = ValidateStackSequences(model.pushed, model.popped);
            return result;
        }
        private bool ValidateStackSequences(int[] pushed, int[] popped)
        {

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < pushed.Length; i++)
            {
                stack.Push(pushed[i]);

                if (pushed[i] == popped[0])
                {
                    stack.Pop();
                    popped = popped.Where(x => x != popped[0]).ToArray();

                    while (stack.Count > 0 && stack.Peek() == popped[0])
                    {
                        stack.Pop();
                        popped = popped.Where(x => x != popped[0]).ToArray();
                    }
                }
            }
            return stack.Count == 0 ? true : false;
        }

        [HttpGet]
        [Route("LongestPalindromeSubseq")] 
        [SwaggerOperation(Tags = new[] { "Input: bbbab >>>>> Output: 4 >>>>>>> Input: cbbd >>>>>>> Output: 2" } )]
        public int LongestPalindromeSubseq(string s)
        {
            int n = s.Length;
            int[,] dp = new int[n, n];

            // diagonal values
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = 1;
            }

            // traversing diagonally
            for (int len = 2; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    if (s[i] == s[j])
                    {
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[0, n - 1];
        }

        [HttpPost]
        [Route("MaxValueOfCoins")]
        [SwaggerOperation(Tags = new[] { "piles = [[1,100,3],[7,8,9]] >>>>>> k = 2 >>>>>>>>>> output: 101" })]
        public int MaxValueOfCoins(IList<IList<int>> piles, int k)
        {
            if (k <= 0)
                return 0;
            int[,] memo = new int[k + 1, piles.Count];

            for (int i = 0; i <= k; ++i)
                for (int j = 0; j < piles.Count; ++j)
                    memo[i, j] = -1;

            return MaxValueOfCoins(piles, 0, k, memo);
        }
        private int MaxValueOfCoins(IList<IList<int>> piles, int curIdx, int k, int[,] memo)
        {
            if (k <= 0 || curIdx >= piles.Count)
                return 0;
            if (memo[k, curIdx] != -1)
                return memo[k, curIdx];

            int coinCount = piles[curIdx].Count;
            int maxPickCount = Math.Min(k, coinCount);

            // option #1 Dont pick coin from this pile
            int result = MaxValueOfCoins(piles, curIdx + 1, k, memo);

            // option #2 Pick from this pile
            int pickSum = 0;
            for (int i = 0; i < maxPickCount; ++i)
            {
                int coinValue = piles[curIdx][i];
                pickSum += coinValue;
                int temp = MaxValueOfCoins(piles, curIdx + 1, k - i - 1, memo);
                result = Math.Max(result, pickSum + temp);

                memo[k, curIdx] = result;
            }

            return result;
        }


        [HttpPost]
        [Route("Reverse")]
        [SwaggerOperation(Tags = new[] { "x = 123 >>>>>> k = 2 >>>>>>>>>> output: 321" })]
        public int Reverse(int x)
        {
            string reverseValue = String.Empty;
            bool isNegativeValue = false;

            if (x > int.MaxValue)
                return 0;

            if(x < 0) 
                isNegativeValue = true;

            string xString = x.ToString();
            int len = !isNegativeValue ? xString.Length - 1 : xString.Length - 2;

            for (int i = xString.Length - 1; i >= (isNegativeValue ? 1 : 0); i--)
            {
                reverseValue += xString[i];
            }

            if (Convert.ToInt64(reverseValue) > int.MaxValue)
                return 0;

            return (isNegativeValue ? -1 : 1) * Convert.ToInt32(reverseValue);
        }


    }
}
