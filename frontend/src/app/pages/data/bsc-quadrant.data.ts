export interface Indicator {
  name: string;
  result: number;
  target: number;
}

export interface BscQuadrant {
  title: string;
  color: string;
  indicators: Indicator[];
}

export const BSC_QUADRANTS: BscQuadrant[] = [
  {
    title: "Financial",
    color: "bg-teal-500",
    indicators: [
      { name: "Indicator 1", result: 7705, target: 8300 },
      { name: "Indicator 2", result: 8491, target: 8400 },
      { name: "Indicator 3", result: 7675, target: 8800 },
    ],
  },
  {
    title: "Customer",
    color: "bg-sky-500",
    indicators: [
      { name: "Indicator 1", result: 8100, target: 8000 },
      { name: "Indicator 2", result: 8073, target: 8400 },
      { name: "Indicator 3", result: 7945, target: 8800 },
    ],
  },
  {
    title: "Internal Business Process",
    color: "bg-gray-700",
    indicators: [
      { name: "Indicator 1", result: 7439, target: 8000 },
      { name: "Indicator 2", result: 8494, target: 8400 },
      { name: "Indicator 3", result: 7674, target: 8800 },
    ],
  },
  {
    title: "Learning & Growth",
    color: "bg-yellow-500",
    indicators: [
      { name: "Indicator 1", result: 7551, target: 7200 },
      { name: "Indicator 2", result: 8114, target: 8400 },
      { name: "Indicator 3", result: 7451, target: 8800 },
    ],
  },
];
