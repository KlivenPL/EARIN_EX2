using EARIN_EX2.Helpers;
using EARIN_EX2.UserInputs;
using Numpy;
using System.Collections;

namespace EARIN_EX2.GeneticAlg {
    public class Individual {
        private readonly GrayHelper grayHelper;
        private readonly NDarray bTranspose;
        private readonly UserInput userInput;

        public Individual(GrayHelper grayHelper, UserInput userInput) {
            this.grayHelper = grayHelper;
            this.userInput = userInput;
            bTranspose = np.transpose(new[] { userInput.B });
        }

        public NDarray X {
            get => grayHelper.GrayToNDarray(GrayedX);
            set => GrayedX = grayHelper.NDarrayToGray(value);
        }

        public BitArray[] GrayedX { get; set; }

        public double Fitness => JFunction(X);

        private double JFunction(NDarray x) {
            var xTranspose = np.transpose(new[] { x });
            var bTx = np.dot(x, bTranspose);
            var xTA = np.dot(userInput.A, xTranspose);
            var xTAx = np.dot(x, xTA);

            var func = userInput.C + bTx + xTAx;
            return (double)func;
        }

        public override string ToString() {
            return $"{X}: {Fitness}";
        }
    }
}
