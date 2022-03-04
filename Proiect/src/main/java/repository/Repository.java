package repository;

import java.util.Collection;

public interface Repository<T, Tid> {
    void add(T el);
    void delete(T el);
    void update(T el,Tid id);
    T findById(Tid id);
    Iterable<T> findAll();
    Collection<T> getAll();
}
