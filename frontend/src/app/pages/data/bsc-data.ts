export interface KPI {
  id: number;
  name: string;
  target: number;
  actual: number;
  unit: string;
}

export interface Objective {
  id: number;
  name: string;
  expanded?: boolean;
  kpis: KPI[];
}

export interface Perspective {
  id: number;
  name: string;
  expanded?: boolean;
  objectives: Objective[];
}

export const BSC_DATA: Perspective[] = [
  {
    id: 1,
    name: "Financial",
    expanded: true,
    objectives: [
      {
        id: 1,
        name: "Increase Revenue",
        expanded: true,
        kpis: [
          { id: 1, name: "Sales Growth", target: 15, actual: 12, unit: "%" },
          {
            id: 2,
            name: "Net Profit Margin",
            target: 20,
            actual: 22,
            unit: "%",
          },
        ],
      },
    ],
  },
  {
    id: 2,
    name: "Customer",
    objectives: [
      {
        id: 2,
        name: "Improve Satisfaction",
        kpis: [
          {
            id: 3,
            name: "Customer Satisfaction Score",
            target: 90,
            actual: 85,
            unit: "%",
          },
        ],
      },
    ],
  },
];
