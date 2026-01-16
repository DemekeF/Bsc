export interface KpiRow {
  name: string;
  actual: number;
  target: number;
  values: number[];
  children?: KpiRow[];
}
export const KPI_DATA: KpiRow[] = [
  {
    name: "Financial",
    actual: 0,
    target: 0,
    values: [],
    children: [
      {
        name: "Net Profit Margin",
        actual: 10,
        target: 10,
        values: [2.4, 3.6, 2.4, 2.4, 4, 4, 3.6, 2.4, 4, 2.4],
      },
      {
        name: "Cost per Unit",
        actual: 15,
        target: 10,
        values: [10, 15, 25, 15, 10, 25, 15, 10, 15, 25],
      },
    ],
  },
  {
    name: "Internal Perspective",
    actual: 0,
    target: 0,
    values: [],
    children: [
      {
        name: "Employee Productivity",
        actual: 80,
        target: 85,
        values: [80, 10, 35, 80, 10, 35, 80, 10, 35, 80],
      },
      {
        name: "Customer Satisfaction (CSAT)",
        actual: 85,
        target: 90,
        values: [85, 10, 85, 10, 35, 85, 10, 85, 10, 35],
      },
    ],
  },
];
