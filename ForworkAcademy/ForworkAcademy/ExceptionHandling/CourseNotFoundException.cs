﻿namespace ForworkAcademy.ExceptionHandling
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(string? message)
            : base(message)
        {
        }
    }
}
