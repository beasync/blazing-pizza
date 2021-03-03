import React from "react";
import { useForm } from "react-hook-form";

type ProductFormProps = {
    name?: string;
    price?: number;
    onSubmit: (data: any) => void;
};

export default function ProductForm({ name, price, onSubmit }: ProductFormProps) {
  const { register, handleSubmit, errors } = useForm();

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input name="name" defaultValue={name} placeholder="Product Name" ref={register({ required: true })} />
      {errors.name && <span>This field is required</span>}

      <input type="number" name="price" defaultValue={price} placeholder="Product Price"  ref={register({ required: true })} />
      {errors.price && <span>This field is required</span>}
      
      <input type="submit" value="Send" />
    </form>
  );
}