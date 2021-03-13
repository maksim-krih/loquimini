import React from "react";
import { Layout } from "../components";

export const withLayout = <T extends object>(Component: React.ComponentType<T>) =>
  (componentProps: T) => (
    <Layout>
      <Component {...componentProps} />
    </Layout>
  );