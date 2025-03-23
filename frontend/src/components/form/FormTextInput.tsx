import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

const FormTextInputSchema = (minLength: number, maxLength: number) =>
  z.object({
    text: z
      .string()
      .min(minLength, `Must be at least ${minLength} characters`)
      .max(maxLength, `Cannot exceed ${maxLength} characters`),
  });

const FormTextInput = ({ minLength = 1, maxLength = 255 }) => {
  const schema = FormTextInputSchema(minLength, maxLength);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({ resolver: zodResolver(schema) });

  return (
    <form onSubmit={handleSubmit(console.log)}>
      <input type="text" {...register("text")} />
      {errors.text && <p>{errors.text.message}</p>}
      <button type="submit">Submit</button>
    </form>
  );
};

export default FormTextInput;
