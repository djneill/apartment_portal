import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';

const FormDateInputSchema = (minDate: Date, maxDate: Date) => z.object({
    date: z.coerce.date()
        .refine((date) => date < maxDate, { message: "Date cannot be in the future" })
        .refine((date) => date > minDate, { message: "Date must be after 1900" })
});

const FormDateInput = ({ minDate = new Date(1900, 0, 1), maxDate = new Date() }) => {
    const schema = FormDateInputSchema(minDate, maxDate);
    const { register, handleSubmit, formState: { errors } } = useForm({ resolver: zodResolver(schema) });

    return (
        <form onSubmit={handleSubmit(console.log)}>
            <input type="date" {...register("date")} />
            {errors.date && <p>{errors.date.message}</p>}
            <button type="submit">Submit</button>
        </form>
    );
};

export default FormDateInput;