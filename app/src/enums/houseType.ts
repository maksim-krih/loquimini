export enum HouseType {
  Private,
  Apartment
}

export const HouseTypeSelect = [
  {
    label: "Private",
    value: HouseType.Private
  },
  {
    label: "Apartment",
    value: HouseType.Apartment
  }
];

export const HouseTypeLabel = (type: HouseType) => {
  switch (type) {
    case HouseType.Private:
      return "Private";
    case HouseType.Apartment:
      return "Apartment";
  }
}